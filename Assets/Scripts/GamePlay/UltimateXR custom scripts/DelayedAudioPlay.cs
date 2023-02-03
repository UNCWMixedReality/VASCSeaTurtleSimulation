using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedAudioPlay : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private int _secondsUntilPlay;
    
    IEnumerator playAudio()
    {
        yield return new WaitForSeconds(_secondsUntilPlay);
        _audio.Play();
    }

    public void startCountdown()
    {
        StartCoroutine(playAudio());
    }
}
