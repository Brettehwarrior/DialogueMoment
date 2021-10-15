using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class DialogueAudio : MonoBehaviour
{    
    private enum AudioMode {
        Mute,
        PlayOnTextScroll,
        RepeatWithDelay
    }

    [SerializeField] AnimatedTMP animatedTMP;

    [SerializeField] private AudioClip[] talkSounds;
    [SerializeField] private AudioMode audioMode;
    [SerializeField] private float delay;

    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();

        // Like and subscribe to event
        switch (audioMode) {
            case AudioMode.PlayOnTextScroll:
                animatedTMP.onTextScroll.AddListener((text) => PlaySound(text));
                break;

            case AudioMode.RepeatWithDelay:
                animatedTMP.onDialogueStart.AddListener(() => StartPlaySoundRepeat());
                animatedTMP.onDialogueFinish.AddListener(() => StopPlaySoundRepeat());
                break;
        }
        
    }

    public void PlaySound(string text) {
        audioSource.PlayOneShot(talkSounds[Random.Range(0, talkSounds.Length)]);
    }

    public void StartPlaySoundRepeat() {
        StartCoroutine(PlaySoundRepeat());
    }

    public void StopPlaySoundRepeat() {
        StopAllCoroutines();
    }

    private IEnumerator PlaySoundRepeat() {
        yield return new WaitForSeconds(delay);
        PlaySound("a");
        StartCoroutine(PlaySoundRepeat());
    }
}
