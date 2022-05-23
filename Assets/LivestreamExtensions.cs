using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class LivestreamExtensions
{

    public static string GetData() {
        if (Camera.main != null)
        {
            // converts texture from current frame of main camera into a base 64 string
            Texture2D rt = CaptureCurrentFrame(Camera.main);
            return Convert.ToBase64String(rt.EncodeToPNG()).TrimEnd('=');
        }
        return "";
    }

    static Texture2D CaptureCurrentFrame(Camera cam) //uses targetTexture of the camera to get a renderTexture, reads the renderTexture to get Texture2D
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

        cam.targetTexture = null;

        return currentFrame;
    }
}
