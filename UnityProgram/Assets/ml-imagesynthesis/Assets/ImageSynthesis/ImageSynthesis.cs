using System;
using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using System.IO;
using Assets.ImageSynthesis;
using Scripts_1;
using Object = UnityEngine.Object;

// @TODO:
// . support custom color wheels in optical flow via lookup textures
// . support custom depth encoding
// . support multiple overlay cameras
// . tests
// . better example scene(s)

// @KNOWN ISSUES
// . Motion Vectors can produce incorrect results in Unity 5.5.f3 when
//      1) during the first rendering frame
//      2) rendering several cameras with different aspect ratios - vectors do stretch to the sides of the screen

[RequireComponent(typeof(Camera))]
public class ImageSynthesis : MonoBehaviour
{
    // pass configuration
    private readonly CapturePass[] capturePasses =
    {
        new CapturePass() {name = "_img"},
        new CapturePass() {name = "_id", supportsAntialiasing = false},
        new CapturePass() {name = "_layer", supportsAntialiasing = false},
        new CapturePass() {name = "_depth"},
        new CapturePass() {name = "_normals"},
        new CapturePass()
        {
            name = "_flow", supportsAntialiasing = false, needsRescale = true
        } // (see issue with Motion Vectors in @KNOWN ISSUES)
    };

    private Camera _defaultCamera;

    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = GetComponent<Camera>();

        var go = new GameObject(name, typeof(Camera)) {hideFlags = HideFlags.HideAndDontSave};
        go.SetActive(false);
        _defaultCamera = go.GetComponent<Camera>();
        _defaultCamera.RemoveAllCommandBuffers();
        _defaultCamera.CopyFrom(_mainCamera);
    }

    struct CapturePass
    {
        // configuration
        public string name;
        public bool supportsAntialiasing;
        public bool needsRescale;

        public CapturePass(string name_)
        {
            name = name_;
            supportsAntialiasing = true;
            needsRescale = false;
            camera = null;
        }

        // impl
        public Camera camera;
    };

    public Shader uberReplacementShader;
    public Shader opticalFlowShader;

    public float opticalFlowSensitivity = 1.0f;

    // cached materials
    private Material opticalFlowMaterial;

    void Start()
    {
        // default fallbacks, if shaders are unspecified
        if (!uberReplacementShader)
            uberReplacementShader = Shader.Find("Hidden/UberReplacement");

        if (!opticalFlowShader)
            opticalFlowShader = Shader.Find("Hidden/OpticalFlow");

        OnCameraChange();
        OnSceneChange();
    }

    void LateUpdate()
    {

    #if UNITY_EDITOR
        if (DetectPotentialSceneChangeInEditor())
            OnSceneChange();
    #endif // UNITY_EDITOR

        // @TODO: detect if camera properties actually changed
        OnCameraChange();
    }

    private static void SetupCameraWithReplacementShader(Camera cam, Shader shader, ReplacelementModes mode)
    {
        SetupCameraWithReplacementShader(cam, shader, mode, Color.black);
    }

    private static void SetupCameraWithReplacementShader(Camera cam, Shader shader, ReplacelementModes mode,
        Color clearColor)
    {
        var cb = new CommandBuffer();
        cb.SetGlobalFloat("_OutputMode", (int) mode); // @TODO: CommandBuffer is missing SetGlobalInt() method
        cam.AddCommandBuffer(CameraEvent.BeforeForwardOpaque, cb);
        cam.AddCommandBuffer(CameraEvent.BeforeFinalPass, cb);
        cam.SetReplacementShader(shader, "");
        cam.backgroundColor = clearColor;
        cam.clearFlags = CameraClearFlags.SolidColor;
    }

    private static void SetupCameraWithPostShader(Camera cam, Material material,
        DepthTextureMode depthTextureMode = DepthTextureMode.None)
    {
        var cb = new CommandBuffer();
        cb.Blit(null, BuiltinRenderTextureType.CurrentActive, material);
        cam.AddCommandBuffer(CameraEvent.AfterEverything, cb);
        cam.depthTextureMode = depthTextureMode;
    }

    enum ReplacelementModes
    {
        ObjectId = 0,
        CatergoryId = 1,
        DepthCompressed = 2,
        DepthMultichannel = 3,
        Normals = 4
    };

    public void OnCameraChange()
    {
        _mainCamera.RemoveAllCommandBuffers();

        Vector3 originalPosition = _mainCamera.transform.position;
        Quaternion originalRotation = _mainCamera.transform.rotation;
        Vector3 originalScale = _mainCamera.transform.localScale;

        // copy all "main" camera parameters into capturing camera
        _mainCamera.CopyFrom(_defaultCamera);

        _mainCamera.transform.position = originalPosition;
        _mainCamera.transform.rotation = originalRotation;
        _mainCamera.transform.localScale = originalScale;

        // cache materials and setup material properties
        if (!opticalFlowMaterial || opticalFlowMaterial.shader != opticalFlowShader)
            opticalFlowMaterial = new Material(opticalFlowShader);
        opticalFlowMaterial.SetFloat("_Sensitivity", opticalFlowSensitivity);

        var cameraEffect = CameraEffectSettings.Instance.CurrentCameraEffect;

        switch (cameraEffect)
        {
            case CameraEffectType.Id:
                SetupCameraWithReplacementShader(_mainCamera, uberReplacementShader,
                    ReplacelementModes.ObjectId);
                break;
            case CameraEffectType.Layer:
                SetupCameraWithReplacementShader(_mainCamera, uberReplacementShader,
                    ReplacelementModes.CatergoryId);
                break;
            case CameraEffectType.Depth:
                SetupCameraWithReplacementShader(_mainCamera, uberReplacementShader,
                    ReplacelementModes.DepthCompressed, Color.white);
                break;
            case CameraEffectType.Normals:
                SetupCameraWithReplacementShader(_mainCamera, uberReplacementShader,
                    ReplacelementModes.Normals);
                break;
            case CameraEffectType.Flow:
                SetupCameraWithPostShader(_mainCamera, opticalFlowMaterial,
                    DepthTextureMode.Depth | DepthTextureMode.MotionVectors);
                break;
        }
    }


    public void OnSceneChange()
    {
        var renderers = Object.FindObjectsOfType<Renderer>();
        var mpb = new MaterialPropertyBlock();
        foreach (var r in renderers)
        {
            var id = r.gameObject.GetInstanceID();
            var layer = r.gameObject.layer;
            var tag = r.gameObject.tag;

            mpb.SetColor("_ObjectColor", ColorEncoding.EncodeIDAsColor(id));
            mpb.SetColor("_CategoryColor", ColorEncoding.EncodeLayerAsColor(layer));
            r.SetPropertyBlock(mpb);
        }
    }

    public void Save(string filename, int width = -1, int height = -1, string path = "")
    {
        if (width <= 0 || height <= 0)
        {
            width = Screen.width;
            height = Screen.height;
        }

        var filenameExtension = System.IO.Path.GetExtension(filename);
        if (filenameExtension == "")
            filenameExtension = ".png";
        var filenameWithoutExtension = Path.GetFileNameWithoutExtension(filename);

        var pathWithoutExtension = Path.Combine(path, filenameWithoutExtension);

        // execute as coroutine to wait for the EndOfFrame before starting capture
        StartCoroutine(
            WaitForEndOfFrameAndSave(pathWithoutExtension, filenameExtension, width, height));
    }

    private IEnumerator WaitForEndOfFrameAndSave(string filenameWithoutExtension, string filenameExtension, int width,
        int height)
    {
        yield return new WaitForEndOfFrame();
        Save(filenameWithoutExtension, filenameExtension, width, height);
    }

    private void Save(string filenameWithoutExtension, string filenameExtension, int width, int height)
    {
        foreach (var pass in capturePasses)
            Save(pass.camera, filenameWithoutExtension + pass.name + filenameExtension, width, height,
                pass.supportsAntialiasing, pass.needsRescale);
    }

    private void Save(Camera cam, string filename, int width, int height, bool supportsAntialiasing, bool needsRescale)
    {
        var mainCamera = GetComponent<Camera>();
        var depth = 24;
        var format = RenderTextureFormat.Default;
        var readWrite = RenderTextureReadWrite.Default;
        var antiAliasing = (supportsAntialiasing) ? Mathf.Max(1, QualitySettings.antiAliasing) : 1;

        var finalRT =
            RenderTexture.GetTemporary(width, height, depth, format, readWrite, antiAliasing);
        var renderRT = (!needsRescale)
            ? finalRT
            : RenderTexture.GetTemporary(mainCamera.pixelWidth, mainCamera.pixelHeight, depth, format, readWrite,
                antiAliasing);
        var tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        var prevActiveRT = RenderTexture.active;
        var prevCameraRT = cam.targetTexture;

        // render to offscreen texture (readonly from CPU side)
        RenderTexture.active = renderRT;
        cam.targetTexture = renderRT;

        cam.Render();

        if (needsRescale)
        {
            // blit to rescale (see issue with Motion Vectors in @KNOWN ISSUES)
            RenderTexture.active = finalRT;
            Graphics.Blit(renderRT, finalRT);
            RenderTexture.ReleaseTemporary(renderRT);
        }

        // read offsreen texture contents into the CPU readable texture
        tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);
        tex.Apply();

        // encode texture into PNG
        var bytes = tex.EncodeToPNG();
        File.WriteAllBytes(filename, bytes);

        // restore state and cleanup
        cam.targetTexture = prevCameraRT;
        RenderTexture.active = prevActiveRT;

        Object.Destroy(tex);
        RenderTexture.ReleaseTemporary(finalRT);
    }

#if UNITY_EDITOR
    private GameObject lastSelectedGO;
    private int lastSelectedGOLayer = -1;
    private string lastSelectedGOTag = "unknown";
    private bool DetectPotentialSceneChangeInEditor()
    {
        bool change = false;
        // there is no callback in Unity Editor to automatically detect changes in scene objects
        // as a workaround lets track selected objects and check, if properties that are 
        // interesting for us (layer or tag) did not change since the last frame
        if (UnityEditor.Selection.transforms.Length > 1)
        {
            // multiple objects are selected, all bets are off!
            // we have to assume these objects are being edited
            change = true;
            lastSelectedGO = null;
        }
        else if (UnityEditor.Selection.activeGameObject)
        {
            var go = UnityEditor.Selection.activeGameObject;
            // check if layer or tag of a selected object have changed since the last frame
            var potentialChangeHappened = lastSelectedGOLayer != go.layer || lastSelectedGOTag != go.tag;
            if (go == lastSelectedGO && potentialChangeHappened)
                change = true;

            lastSelectedGO = go;
            lastSelectedGOLayer = go.layer;
            lastSelectedGOTag = go.tag;
        }

        return change;
    }
#endif // UNITY_EDITOR
}