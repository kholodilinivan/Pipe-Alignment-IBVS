using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class QuadCopter_new : MonoBehaviour
{
    RenderTexture CameraRender;
    Camera[] ConnectedCameras;
    byte[] EncodedPng;
    int Height;
    int Width;
    bool ScreenshotDone;
    string CompleteFilePath;

    // Use this for initialization
    void Start()
    {
        ConnectedCameras = gameObject.GetComponentsInChildren<UnityEngine.Camera>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CaptureImage(string CameraSelect, int UsrWidth, int UsrHeight)
    {
        ScreenshotDone = false;
        Camera SelectedCamera = null;
        this.Width = UsrWidth;
        this.Height = UsrHeight;

        for (int i = 0; i < ConnectedCameras.Length; i++)
        {
            if (ConnectedCameras[i].name == CameraSelect)
            {
                SelectedCamera = ConnectedCameras[i];
            }
        }

        if (SelectedCamera != null)
        {
            StartCoroutine(GetRender(SelectedCamera));
        }

    }

    public int GetWidth()
    {
        return this.Width;
    }

    public int GetHeight()
    {
        return this.Height;
    }

    public bool CaptureDone()
    {
        return ScreenshotDone;
    }

    public byte[] ReturnCaptureBytes()
    {
        return EncodedPng;
    }

    IEnumerator GetRender(Camera cam)
    {
        this.CameraRender = new RenderTexture(this.Width, this.Height, 16);
        cam.enabled = true;
        cam.rect = new Rect(new Vector2(0, 0), new Vector2(1, 1)); // Without this, the screenshot will have the wrong size
        cam.targetTexture = CameraRender;
        Texture2D tempTex = new Texture2D(CameraRender.width, CameraRender.height, TextureFormat.RGB24, false);
        cam.Render();
        RenderTexture.active = CameraRender;//Sets the Render
        tempTex.ReadPixels(new Rect(0, 0, CameraRender.width, CameraRender.height), 0, 0);
        tempTex.Apply();
        EncodedPng = tempTex.GetRawTextureData();
        Destroy(tempTex);
        yield return null;
        CameraRender.Release();
        ScreenshotDone = true;
        cam.enabled = false;
    }

    private void OnApplicationQuit()
    {
        //cam.enabled = false;
    }
}