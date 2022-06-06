namespace FrostweepGames.Plugins.GoogleCloud.TextToSpeech
{
    public class Enumerators
    {
        public enum GoogleCloudRequestType
        {
            GET_VOICES,
            SYNTHESIZE
        }

        public enum SsmlVoiceGender
        {
            SSML_VOICE_GENDER_UNSPECIFIED,
            MALE,
            FEMALE,
            NEUTRAL
        }

        public enum AudioEncoding
        {
            AUDIO_ENCODING_UNSPECIFIED,
            LINEAR16,
            MP3,
            OGG_OPUS
        }

        public enum LanguageCode
        {
            en_AU,
            nl_NL,
            en_GB,
            en_US,
            fr_FR,
            fr_CA,
            de_DE,
            it_IT,
            ja_JP,
            ko_KR,
            pt_BR,
            es_ES,
            sv_SE,
            tr_TR
        }

        public enum VoiceType
        {
            WAVENET,
            STANDARD
        }
    }
}