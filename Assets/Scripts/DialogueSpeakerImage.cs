using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSpeakerImage : MonoBehaviour
{
    [SerializeField] AnimatedTMP animatedTMP;
    [SerializeField] RawImage image;


    [SerializeField] private Texture[] images;
    private int imageIndex;
    [SerializeField] private float delay;

    private void Start() {
        animatedTMP.onDialogueStart.AddListener(() => StartAnimation());
        animatedTMP.onDialogueFinish.AddListener(() => StopAnimation());
    }

    public void StartAnimation() {
        StartCoroutine(Animation());
    }

    public void StopAnimation() {
        imageIndex = 0;
        image.texture = images[0];
        StopAllCoroutines();
    }

    private IEnumerator Animation() {
        yield return new WaitForSeconds(delay);
        imageIndex = (imageIndex < images.Length - 1) 
            ? imageIndex + 1
            : 0;

        image.texture = images[imageIndex];
        StartCoroutine(Animation());
    }
}
