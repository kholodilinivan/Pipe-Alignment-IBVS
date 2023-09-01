using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using SimpleFileBrowser;
using Assets.ImageSynthesis;
using Scripts_1;
using System.Collections.ObjectModel;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class QuadCopter : MonoBehaviour {
    public GameObject[] dronConfigs;
    public Changes changesScript;

    private List<CameraAutosize> cameraAutosizeScripts; 

    Camera camL, camR, camC;
    RenderTexture CameraRender;
    byte[] EncodedPng, EncodedJpg;
    public int Height;
    public int Width;
    bool ScreenshotDone;
    string CompleteFilePath;

    // for Canvas Settings
    public InputField WidthCam, HeightCam, BrowsePath, imgName;

    // for Canvas Settings
    public InputField WidthCam_1, HeightCam_1, BrowsePath_1, imgName_1;

    private int a, b;

    // for changing state between canvases
    public GameObject walleState, CalibState;
    public Dropdown ImgExtent, ImgExtent_1;

    // Use this for initialization
    void Start () 
    {
        a = 0;
        b = 0;
        ScreenshotDone = true;

        cameraAutosizeScripts = new List<CameraAutosize>();

        for(int i = 0; i < dronConfigs.Length; i++) {
            CameraAutosize scriptInCurrentDronConfig = dronConfigs[i].GetComponent<CameraAutosize>();

            if (scriptInCurrentDronConfig != null) {
                cameraAutosizeScripts.Add(scriptInCurrentDronConfig);
            }

        }
    }
	
	// Update is called once per frame
	void Update () {
        
        if (walleState.activeSelf == true)
        {
            Width = int.Parse(WidthCam.text);
            Height = int.Parse(HeightCam.text);
        }
        else if (CalibState.activeSelf == true)
        {
            Width = int.Parse(WidthCam_1.text);
            Height = int.Parse(HeightCam_1.text); 
        }

        CameraRender = new RenderTexture(Width, Height, 16);

        camL = GameObject.Find("CamL").GetComponent<Camera>();
        camL.enabled = false;

        camR = GameObject.Find("CamR").GetComponent<Camera>();
        camR.enabled = false;

        camC = GameObject.Find("CamC").GetComponent<Camera>();
        camC.enabled = false;
	}

    public void CaptureImage(string FilePath, string Filename, string CameraSelect)
    {
        Camera SelectedCamera;
        ScreenshotDone = false;

        if (CameraSelect == "Left")
        {
            SelectedCamera = camL;
        }
        else if(CameraSelect == "Right")
        {
            SelectedCamera = camR;
        }
        else
        {
            SelectedCamera = camC;
        }

        CompleteFilePath = FilePath + "/" + Filename;
        Debug.Log("File was Saved at:" + CompleteFilePath + ".png");
        StartCoroutine(GetRender(SelectedCamera));
        
    }

    public void BrowseLocation()
    {
        StartCoroutine(ShowLoadDialogCoroutine());
    }

    public void SaveSnapshot()
    {
        a = 1;

        Camera SelectedCamera;

        SelectedCamera = camC;

        //#if UNITY_EDITOR
        //        CompleteFilePath = EditorUtility.SaveFolderPanel("Save textures to folder", "", "") + "/" + "test";
        //#endif

        if (walleState.activeSelf == true)
        {
            CompleteFilePath = BrowsePath.text + "/" + imgName.text;

            if (ImgExtent.value == 0)
            {
                b = 0;
            }
            else if (ImgExtent.value == 1)
            {
                b = 1;
            }
        }
        else if (CalibState.activeSelf == true)
        {
            CompleteFilePath = BrowsePath_1.text + "/" + imgName_1.text;

            if (ImgExtent_1.value == 0)
            {
                b = 0;
            }
            else if (ImgExtent_1.value == 1)
            {
                b = 1;
            }
        }
     //   Debug.Log("File was Saved at:" + CompleteFilePath + ".png");

        StartCoroutine(GetRender(SelectedCamera));
    }

    public bool CaptureDone()
    {
        return ScreenshotDone;
    }

    public byte[] ReturnCaptureBytes()
    {
        return EncodedPng;
    }

    IEnumerator ShowLoadDialogCoroutine()
    {
        yield return FileBrowser.ShowLoadDialog((path) => 
        { 
            Debug.Log("Selected: " + path); 
            BrowsePath.text = path;
            BrowsePath_1.text = path;
        },
        () => { Debug.Log("Canceled"); },
        true, null, "Select Folder", "Select");
    }

    IEnumerator GetRender(Camera cam)
    {
        while(!CaptureDone()) 
        {
            yield return null;
        }
        
        ScreenshotDone = false;

        Texture2D tempTex = new Texture2D(CameraRender.width, CameraRender.height, TextureFormat.RGB24, false);
        CameraEffectType cameraEffectBeforeScreenshots = CameraEffectSettings.Instance.CurrentCameraEffect;
        cam.enabled = true;

        ReadOnlyCollection<CameraEffectType> effectsForScreenshots = ScreenshotsEffectsManager.getAllEffectsForScreenshots();
        string[] screenshotsFilesSufixes;

        if (effectsForScreenshots.Count > 0) {
            screenshotsFilesSufixes = new string[effectsForScreenshots.Count];

            for (int i = 0; i < effectsForScreenshots.Count; i++) {
                screenshotsFilesSufixes[i] = $"_{((int)effectsForScreenshots[i]) + 1}";
            }
        }
        else {
            effectsForScreenshots = ScreenshotsEffectsManager.GetDefaultEffects();
            screenshotsFilesSufixes = new string[effectsForScreenshots.Count];

            for (int i = 0; i < effectsForScreenshots.Count; i++) {
                screenshotsFilesSufixes[i] = "";
            }
        }

        cam.rect = new Rect(new Vector2(0, 0), new Vector2(1, 1)); // Without this, the screenshot will have the wrong size

        for (int i = 0; i < effectsForScreenshots.Count; i++) 
        {
            CameraEffectSettings.Instance.CurrentCameraEffect = effectsForScreenshots[i];
            changesScript.EffectButtons();

            yield return null;

            cam.targetTexture = CameraRender;
            cam.Render();

            RenderTexture.active = CameraRender;//Sets the Render
            tempTex.ReadPixels(new Rect(0, 0, CameraRender.width, CameraRender.height), 0, 0);

            tempTex.Apply();
        
            if (a > 0) 
            {
                if (b == 0)
                {
                    File.WriteAllBytes(CompleteFilePath + screenshotsFilesSufixes[i] + ".jpg", tempTex.EncodeToJPG());
                }
                else if (b == 1)
                {
                    File.WriteAllBytes(CompleteFilePath + screenshotsFilesSufixes[i] + ".png", tempTex.EncodeToPNG());
                }
            }
        }

        CameraEffectSettings.Instance.CurrentCameraEffect = cameraEffectBeforeScreenshots;
        changesScript.EffectButtons();
       
        Destroy(tempTex);

        if (a > 0)
        {
            a = 0;
        }
       
        CameraRender.Release();
        ScreenshotDone = true;
        cam.enabled = false;

        foreach (CameraAutosize script in cameraAutosizeScripts) {
            script.UpdateCameraSize(); //Resizing
        }
    }

    private void OnApplicationQuit()
    {
        //cam.enabled = false;
    }

    // Image extention
    public void chooseImgExtention()
    {
        if (walleState.activeSelf == true)
        {
            
        }
        else if (CalibState.activeSelf == true)
        {
            
        }
    }
}
