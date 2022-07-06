using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdentifyInstructions : MonoBehaviour
{
    public string loggerheadFact;
    public string hawksbillFact;
    public string leatherbackFact;

    public AudioClip loggerheadAudio;
    public AudioClip hawksbillAudio;
    public AudioClip leatherbackAudio;

    public NewInstructionAudioM1 instrAudio;
    public NewInstructionUpdaterM1 instrText;

    public void SetInstructions(int loggerheadIdx, int hawksbillIdx, int leatherbackIdx)
    {
        instrAudio.audioInstructions[loggerheadIdx + 13].clip = loggerheadAudio;
        instrText.instructions[loggerheadIdx + 13] += loggerheadFact;

        instrAudio.audioInstructions[hawksbillIdx + 13].clip = hawksbillAudio;
        instrText.instructions[hawksbillIdx + 13] += hawksbillFact;

        instrAudio.audioInstructions[leatherbackIdx + 13].clip = leatherbackAudio;
        instrText.instructions[leatherbackIdx + 13] += leatherbackFact;

        instrText.instructions[13] += ("Now identify the next turtle");
        instrText.instructions[14] += ("Now identify the next turtle");
        instrText.instructions[15] += ("Move to the blue waypoint to start the next task.");

    }
}
