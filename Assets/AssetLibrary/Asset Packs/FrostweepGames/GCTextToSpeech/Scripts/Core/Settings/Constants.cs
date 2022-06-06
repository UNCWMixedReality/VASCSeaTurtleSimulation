namespace FrostweepGames.Plugins.GoogleCloud.TextToSpeech
{
    public class Constants
    {
        public const string API_VERSION = "v1";

        public const string POST_TEXT_SYNTHESIZE = "https://texttospeech.googleapis.com/" + API_VERSION + "/text:synthesize";
        public const string GET_LIST_VOICES = "https://texttospeech.googleapis.com/" + API_VERSION + "/voices";

        public const string API_KEY_PARAM = "?key=";


        public const string GC_API_KEY = ""; // Google Cloud API Key


        public const Enumerators.AudioEncoding DEFAULT_AUDIO_ENCODING = Enumerators.AudioEncoding.LINEAR16;
        public const double DEFAULT_SAMPLE_RATE = 16000;
        public const double DEFAULT_VOLUME_GAIN_DB = 0.0;
    }
}