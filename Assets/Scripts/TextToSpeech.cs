using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FrostweepGames.Plugins.GoogleCloud.TextToSpeech;
using System.Linq;
using System.Text.RegularExpressions;
using System;

public class TextToSpeech : Singleton<TextToSpeech>
{
    AudioSource audioSource;
    GCTextToSpeech textToSpeech;
    Voice[] voices;
    Voice currentVoice;
    const string highlightTagStart = "<mark=#108AFF50>";
    const string tagEnd = "</mark>";

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        textToSpeech = gameObject.AddComponent<GCTextToSpeech>();
        textToSpeech.apiKey = "AIzaSyA_FsRKbXEdOAh_ipfNQiHhg_euAmYVcUI";
        textToSpeech.SynthesizeSuccessEvent += SynthesizeSuccessEvent;
        textToSpeech.GetVoicesSuccessEvent += GetVoicesSuccessEvent;
        textToSpeech.isFullDebugLogIfError = true;
        textToSpeech.isUseAPIKeyFromPrefab = true;
        textToSpeech.GetVoices(new GetVoicesRequest() { languageCode = "en_US" });
    }

    private void GetVoicesSuccessEvent(GetVoicesResponse response)
    {
        voices = response.voices;
        currentVoice = voices.ToList().Find(item => item.name == "en-US-Wavenet-A");
    }

    void SynthesizeSuccessEvent(PostSynthesizeResponse response)
    {
        audioSource.clip = textToSpeech.GetAudioClipFromBase64(response.audioContent, Constants.DEFAULT_AUDIO_ENCODING);
        audioSource.Play();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    TextMeshProUGUI targetTextMesh;
    Coroutine speakCoroutine;
    public void Speak(TextMeshProUGUI textMesh)
    {
        if (speakCoroutine != null)
        {
            StopCoroutine(speakCoroutine);
            targetTextMesh.text = targetTextMesh.text.Replace(highlightTagStart, null).Replace(tagEnd, null);
            audioSource.Stop();
        }
        speakCoroutine = StartCoroutine(CoSpeak(textMesh));
    }

    public static string StripHTML(string input)
    {
        return Regex.Replace(input, "<.*?>", String.Empty);
    }

    IEnumerator CoSpeak(TextMeshProUGUI textMesh)
    {
        targetTextMesh = textMesh;
        var text = StripHTML(targetTextMesh.text);
        while (currentVoice == null)
            yield return new WaitForSeconds(0);

        audioSource.clip = null;
        textToSpeech.Synthesize(text, new VoiceConfig()
        {
            gender = currentVoice.ssmlGender,
            languageCode = currentVoice.languageCodes[0],
            name = currentVoice.name
        },
            false,
            1,
            1,
            currentVoice.naturalSampleRateHertz);

        while (audioSource.clip == null)
            yield return new WaitForSeconds(0);

        var charLength = (audioSource.clip.length - .85f) / (float)text.Length;
        for (int i = 0; i < text.Length; i++)
        {
            textMesh.text = highlightTagStart + text.Substring(0, i + 1) + tagEnd + text.Substring(i + 1, text.Length - 1 - i);
            yield return new WaitForSecondsRealtime(charLength);
        }
    }
}
