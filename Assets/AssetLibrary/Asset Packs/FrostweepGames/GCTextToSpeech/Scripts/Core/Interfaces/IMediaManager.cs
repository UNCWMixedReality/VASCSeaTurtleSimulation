using UnityEngine;

namespace FrostweepGames.Plugins.GoogleCloud.TextToSpeech
{
    public interface IMediaManager
    {
        AudioClip GetAudioClipFromBase64String(string base64String, Enumerators.AudioEncoding audioEncoding);
        void SaveAudioFileAsFile(string content, string path, string fileName, Enumerators.AudioEncoding audioEncoding);
    }
}