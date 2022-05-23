namespace FrostweepGames.Plugins.GoogleCloud.TextToSpeech
{
    #region requests

    public class GetVoicesRequest
    {
        public string languageCode;
    }

    public class PostSynthesizeRequest
    {
        public SynthesisInput input;
        public VoiceSelectionParams voice;
        public AudioConfig audioConfig;
    }

    #endregion

    #region models

    public class SynthesisInput
    {
    }

    public class SynthesisInputText : SynthesisInput
{
        public string text;
    }

    public class SynthesisInputSSML : SynthesisInput
{
        public string ssml;
    }

    public class VoiceSelectionParams
    {
        public string languageCode;
        public string name;
        public Enumerators.SsmlVoiceGender ssmlGender;
    }

    public class AudioConfig
    {

        public Enumerators.AudioEncoding audioEncoding;
        public double speakingRate;
        public double pitch;
        public double volumeGainDb;
        public double sampleRateHertz;
    }

    public class Voice
    {
        public string[] languageCodes;
        public string name;
        public Enumerators.SsmlVoiceGender ssmlGender;
        public double naturalSampleRateHertz;
    }

    #endregion

    #region responses

    public class GetVoicesResponse
    {
        public Voice[] voices;
    }

    public class PostSynthesizeResponse
    {
        public string audioContent; // base64 string
    }

    #endregion

    public class VoiceConfig
    {
        public string languageCode;
        public string name;
        public Enumerators.SsmlVoiceGender gender;
    }
}