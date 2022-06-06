using System;

namespace FrostweepGames.Plugins.GoogleCloud.TextToSpeech
{
    public interface ITextToSpeechManager
    {
        event Action<GetVoicesResponse> GetVoicesSuccessEvent;
        event Action<PostSynthesizeResponse> SynthesizeSuccessEvent;

        event Action<string> GetVoicesFailedEvent;
        event Action<string> SynthesizeFailedEvent;

        string PrepareLanguage(Enumerators.LanguageCode lang);

        void GetVoices(GetVoicesRequest getVoicesRequest);
        void Synthesize(PostSynthesizeRequest synthesizeRequest);
    }
}