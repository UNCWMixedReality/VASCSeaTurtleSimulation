using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackInstructions : MonoBehaviour
{
    public string loggerheadFact;
    public string greenFact;
    public string leatherbackFact;

    public AudioClip loggerheadAudio;
    public AudioClip greenAudio;
    public AudioClip leatherbackAudio;

    public NewInstructionAudioM1 instrAudio;
    public NewInstructionUpdaterM1 instrText;

    public void SetInstructions(int loggerheadIdx, int greenIdx, int leatherbackIdx)
    {
        instrAudio.audioInstructions[loggerheadIdx + 18].clip = loggerheadAudio;
        instrText.instructions[loggerheadIdx + 18] += loggerheadFact;

        instrAudio.audioInstructions[greenIdx + 18].clip = greenAudio;
        instrText.instructions[greenIdx + 18] += greenFact;

        instrAudio.audioInstructions[leatherbackIdx + 18].clip = leatherbackAudio;
        instrText.instructions[leatherbackIdx + 18] += leatherbackFact;

        instrText.instructions[18] += (" Now identify the next set of tracks");
        instrText.instructions[19] += (" Now identify the next set of tracks");
        instrText.instructions[20] += (" You have successfully identified all the tracks.");

    }
}
