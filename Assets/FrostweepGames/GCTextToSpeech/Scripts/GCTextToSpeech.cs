using System;
using UnityEngine;

namespace FrostweepGames.Plugins.GoogleCloud.TextToSpeech
{
    public class GCTextToSpeech : MonoBehaviour
    {
        public event Action<GetVoicesResponse> GetVoicesSuccessEvent;
        public event Action<PostSynthesizeResponse> SynthesizeSuccessEvent;

        public event Action<string> GetVoicesFailedEvent;
        public event Action<string> SynthesizeFailedEvent;


        private static GCTextToSpeech _Instance;
        public static GCTextToSpeech Instance
        {
            get
            {
                if (_Instance == null)
                {
                    var obj = Resources.Load<GameObject>("Prefabs/GCTextToSpeech");

                    if (obj != null)
                    {
                        obj.name = "[Singleton]GCTextToSpeech";
                        _Instance = obj.GetComponent<GCTextToSpeech>();
                    }
                    else
                        _Instance = new GameObject("[Singleton]GCTextToSpeech").AddComponent<GCTextToSpeech>();
                }

                return _Instance;
            }
        }

        private ServiceLocator _serviceLocator;

        private ITextToSpeechManager _textToSpeechManager;
        private IMediaManager _mediaManager;

        public ServiceLocator ServiceLocator { get { return _serviceLocator; } }

        [Header("Prefab Object Settings")]
        public bool isDontDestroyOnLoad = false;
        public bool isFullDebugLogIfError = false;
        public bool isUseAPIKeyFromPrefab = false;

        [Header("Prefab Fields")]
        public string apiKey = string.Empty;

        private void Awake()
        {
            if (_Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            if (isDontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);

            _Instance = this;

            _serviceLocator = new ServiceLocator();
            _serviceLocator.InitServices();

            _textToSpeechManager = _serviceLocator.Get<ITextToSpeechManager>();
            _mediaManager = _serviceLocator.Get<IMediaManager>();

            _textToSpeechManager.GetVoicesSuccessEvent += GetVoicesSuccessEventHandler;
            _textToSpeechManager.SynthesizeSuccessEvent += SynthesizeSuccessEventHandler;

            _textToSpeechManager.GetVoicesFailedEvent += GetVoicesFailedEventHandler;
            _textToSpeechManager.SynthesizeFailedEvent += SynthesizeFailedEventHandler;
        }

        private void Update()
        {
            if (_Instance == this)
            {
                _serviceLocator.Update();
            }
        }

        private void OnDestroy()
        {
            if (_Instance == this)
            {
                _textToSpeechManager.GetVoicesSuccessEvent -= GetVoicesSuccessEventHandler;
                _textToSpeechManager.SynthesizeSuccessEvent -= SynthesizeSuccessEventHandler;

                _textToSpeechManager.GetVoicesFailedEvent -= GetVoicesFailedEventHandler;
                _textToSpeechManager.SynthesizeFailedEvent -= SynthesizeFailedEventHandler;

                _Instance = null;
                _serviceLocator.Dispose();
            }
        }

        public string PrepareLanguage(Enumerators.LanguageCode lang)
        {
            return _textToSpeechManager.PrepareLanguage(lang);
        }

        public AudioClip GetAudioClipFromBase64(string value, Enumerators.AudioEncoding audioEncoding)
        {
           return _mediaManager.GetAudioClipFromBase64String(value, audioEncoding);
        }

        public void GetVoices(GetVoicesRequest getVoicesRequest)
        {
            _textToSpeechManager.GetVoices(getVoicesRequest);
        }

        public void Synthesize(string content, VoiceConfig voiceConfig, bool ssml = false, double pitch = 1.0, double speakingRate = 1.0, double sampleRateHertz = Constants.DEFAULT_SAMPLE_RATE)
        {
            SynthesisInput synthesisInput = null;

            if (ssml)
                synthesisInput = new SynthesisInputSSML() { ssml = content };
            else
                synthesisInput = new SynthesisInputText() { text = content };

            _textToSpeechManager.Synthesize(new PostSynthesizeRequest()
            {
                audioConfig = new AudioConfig()
                {
                    audioEncoding = Constants.DEFAULT_AUDIO_ENCODING,
                    pitch = pitch,
                    sampleRateHertz = sampleRateHertz,
                    speakingRate = speakingRate,
                    volumeGainDb = Constants.DEFAULT_VOLUME_GAIN_DB
                },
                input = synthesisInput,
                voice = new VoiceSelectionParams()
                {
                    languageCode = voiceConfig.languageCode,
                    name = voiceConfig.name,
                    ssmlGender = voiceConfig.gender
                }
            });
        }

        private void GetVoicesFailedEventHandler(string obj)
        {
            if (GetVoicesFailedEvent != null)
                GetVoicesFailedEvent(obj);
        }

        private void SynthesizeFailedEventHandler(string obj)
        {
            if (SynthesizeFailedEvent != null)
                SynthesizeFailedEvent(obj);
        }

        private void GetVoicesSuccessEventHandler(GetVoicesResponse obj)
        {
            if (GetVoicesSuccessEvent != null)
                GetVoicesSuccessEvent(obj);
        }


        private void SynthesizeSuccessEventHandler(PostSynthesizeResponse obj)
        {
            if (SynthesizeSuccessEvent != null)
                SynthesizeSuccessEvent(obj);
        }
    }
}