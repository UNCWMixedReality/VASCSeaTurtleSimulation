using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CameraCapture : MonoBehaviour
{
    public static CameraCapture instance; //static instance

    public Camera mainCamera; //main camera - add to player prefab or vr controller
    public Camera leftCamera; //left camera - add to player prefab or vr controller
    public Camera rightCamera; //right camera - add to player prefab or vr controller
    public Camera secondaryCamera; //seperate camera

    private Camera CapturingCamera;

    public enum specifyCamera { Primary = 0, Left = 1, Right = 2, Secondary = 3 };
    public specifyCamera currentCamera;

    private bool captureFrame = false; //captures screenshot of camera if true
    private bool captureVideo = false; //captures video if true
    private bool capturing = false; //true if capturing video 

    public Texture2D frame; //screenshot - only public to see result in inspector easily
    public RenderTexture frameRenderTexture; //"video" - renderTexture - only public to see result in inspector easily

    public Camera lastCamera;
    public Transform lastParent;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (mainCamera.isActiveAndEnabled)
            mainCamera.enabled = false;
        else if (leftCamera.isActiveAndEnabled)
            leftCamera.enabled = false;
        else if (rightCamera.isActiveAndEnabled)
            rightCamera.enabled = false;
        else if (secondaryCamera.isActiveAndEnabled)
            secondaryCamera.enabled = false;
    }

    /// <summary>
    /// Returns Texture2D of current frame in game. Int specifies camera: 0 - primary, 1 - left, 2 - right, 3 - secondary. **Call with coroutine and WaitForEndofFrame()
    /// </summary>
    public Texture2D GetCurrentFrame(int camera)
    {
        currentCamera = (specifyCamera)camera; //upcast int to enum
        captureFrame = true;

        Capture();
        
        return frame;
    }

    /// <summary>
    /// Returns RenderTexture of current frame in game. Int specifies camera: 0 - primary, 1 - left, 2 - right, 3 - secondary.**Call with coroutine and WaitForEndofFrame()
    /// </summary>
    public RenderTexture GetVideoFeed(int camera)
    {
        currentCamera = (specifyCamera)camera;
        captureVideo = true;

        Capture();

        return frameRenderTexture;
    }

    public void StopVideoCapturePublic(int camera)
    {
        switch ((specifyCamera)camera) //select the camera to capture from
        {
            case (specifyCamera.Primary):
                StopVideoCapture(mainCamera);
                break;
            case (specifyCamera.Left):
                StopVideoCapture(leftCamera);
                break;
            case (specifyCamera.Right):
                StopVideoCapture(rightCamera);
                break;
            case (specifyCamera.Secondary):
                StopVideoCapture(secondaryCamera);
                break;
        }
    }

    /// <summary>
    /// Stops video feed.
    /// </summary>
    private void StopVideoCapture(Camera cam)
    {
        //cam.targetTexture = null;

        if (lastCamera != null)
        {
            lastCamera.transform.parent = lastParent;
            lastCamera.targetTexture = null;  
        }
            
        capturing = false;
        CapturingCamera.enabled = false;
    }

    private void Capture()
    {
        switch (currentCamera) //select the camera to capture from
        {
            case (specifyCamera.Primary):
                CapturingCamera = mainCamera;
                break;
            case (specifyCamera.Left):
                CapturingCamera = leftCamera;
                break;
            case (specifyCamera.Right):
                CapturingCamera = rightCamera;
                break;
            case (specifyCamera.Secondary):
                CapturingCamera = secondaryCamera;
                break;
        }
        CapturingCamera.enabled = true;

        if (capturing) 
            StopVideoCapture(CapturingCamera);

        if (captureFrame) //screenshot
        {
            frame = CaptureCurrentFrame(CapturingCamera);
            Debug.Log("Captured Frame - " + currentCamera.ToString());

            captureFrame = false;
        }
        else if (captureVideo) //video
        {
            capturing = true;

            frameRenderTexture = StartVideoCapture(CapturingCamera);
            Debug.Log("Capturing Video - " + currentCamera.ToString());

            captureVideo = false;
        }
    }

    private Texture2D CaptureCurrentFrame(Camera cam) //uses targetTexture of the camera to get a renderTexture, reads the renderTexture to get Texture2D
    {
        cam.targetTexture = new RenderTexture(cam.pixelWidth, cam.pixelHeight, 16);

        RenderTexture renderTexture = cam.targetTexture;
        renderTexture.name = "frame";
        RenderTexture.active = renderTexture;
        cam.Render();

        Texture2D currentFrame = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
        currentFrame.name = "frameTexture";

        currentFrame.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        currentFrame.Apply();

        if (currentCamera != specifyCamera.Secondary)
            cam.targetTexture = null;

        return currentFrame;
    }

    private RenderTexture StartVideoCapture(Camera cam)
    {
        if (currentCamera != specifyCamera.Secondary) //if using a camera that needs to follow the user, the camera will remain in one place unless you parent it. If you directly parent the camera in the editor, the camera you are trying to record will be offset from main camera.
        {
            lastCamera = cam;
            lastParent = cam.transform.parent;
            cam.transform.parent = Camera.main.transform;//parents to main Camera - camera with tag "MainCamera" - make sure this is set correctly in editor
        }
        else
        {
            lastCamera = cam;
            lastParent = null;
        }
            

        cam.targetTexture = new RenderTexture(cam.pixelWidth, cam.pixelHeight, 16);

        RenderTexture frameRenderTexture = cam.targetTexture;
        frameRenderTexture.name = "frameRenderTexture";
        RenderTexture.active = frameRenderTexture;
        frameRenderTexture.Create();
        cam.Render();

        return frameRenderTexture;
    }

    private void SaveScreenShot(Texture2D tex) //saves screenshot to "CameraCapture" folder in assets. here if needed, but otherwise unused
    {
        byte[] byteArray = tex.EncodeToPNG();
        System.IO.File.WriteAllBytes(Application.dataPath + "/CameraCapture/cameraFrame.png", byteArray);
        Debug.Log("Screenshot saved.");
    }
}
