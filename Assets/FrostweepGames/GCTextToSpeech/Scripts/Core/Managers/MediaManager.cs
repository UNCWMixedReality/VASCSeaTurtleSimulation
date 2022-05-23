using System;
using System.IO;
using UnityEngine;

namespace FrostweepGames.Plugins.GoogleCloud.TextToSpeech
{
    public class MediaManager : IService, IDisposable, IMediaManager
    {
        public void Dispose()
        {
        }

        public void Init()
        {
        }

        public void Update()
        {
        }


        public AudioClip GetAudioClipFromBase64String(string base64String, Enumerators.AudioEncoding audioEncoding)
        {
            AudioClip audioClip = null;

            switch (audioEncoding)
            {
                case Enumerators.AudioEncoding.LINEAR16:
                    audioClip = ConvertBase64StringToAudioClip(base64String);

                   // SaveAudioFileAsFile(base64String, "C:/", "audioclip", Enumerators.AudioEncoding.LINEAR16);
                    break;
                default:
                    throw new NotImplementedException("Error: Audio Encoding type " + audioEncoding + " isn't implemented!");
            }

            return audioClip;
        }

        public void SaveAudioFileAsFile(string content, string path, string fileName, Enumerators.AudioEncoding audioEncoding)
        {
            string resolution = string.Empty;
            switch(audioEncoding)
            {
                case Enumerators.AudioEncoding.LINEAR16:
                    resolution = ".wav";
                    break;
                case Enumerators.AudioEncoding.MP3:
                    resolution = ".mp3";
                    break;
                case Enumerators.AudioEncoding.OGG_OPUS:
                    resolution = ".ogg";
                    break;
                case Enumerators.AudioEncoding.AUDIO_ENCODING_UNSPECIFIED:
                default: return;
            }

            File.WriteAllBytes(Path.Combine(path, fileName + resolution), Convert.FromBase64String(content));
        }

        private AudioClip ConvertBase64StringToAudioClip(string value)
        {
            return Linear16AudioTool.ToAudioClip(Convert.FromBase64String(value));
        }
    }
}