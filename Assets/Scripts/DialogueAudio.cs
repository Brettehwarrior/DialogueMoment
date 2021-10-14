using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ScrollText))]
public class DialogueAudio : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] talkSounds;
    [SerializeField]
    private float soundDelay;

    private AudioSource audioSource;

    private Coroutine audioCoroutine;
    
    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void StopSounds() {
        StopCoroutine(audioCoroutine);
    }

    public void StartSounds() {
        audioCoroutine = StartCoroutine("PlaySounds");
    }

    private IEnumerator PlaySounds() {
        yield return new WaitForSeconds(soundDelay);
        audioSource.PlayOneShot(talkSounds[Random.Range(0, talkSounds.Length)]);
        StartSounds();
    }
}
