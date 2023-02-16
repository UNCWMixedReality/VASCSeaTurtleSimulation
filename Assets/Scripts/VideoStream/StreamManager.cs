using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.RenderStreaming.Signaling;
using UnityEngine;

public class StreamManager : MonoBehaviour
{

    public Unity.RenderStreaming.RenderStreaming StreamRender;

    // Start is called before the first frame update
    async void Awake()
    {
        (bool, string) results = await APIManager.GetHeadsetNickname(SystemInfo.deviceUniqueIdentifier);

        if (!results.Item1)
        {
            LogManager.LogMessage(results.Item2, true);
        } else
        {
            LogManager.LogMessage(results.Item2);
            string[] vals = results.Item2.Split(' ');
            string WebSafeName = String.Join("", vals).ToLower();
            StreamRender.Run(hardwareEncoder: false,
                new WebSocketSignaling($"ws://{WebSafeName}.stream.capstone.doublel.studio",
                5, SynchronizationContext.Current));
        }
    }

}
