using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnimatedTMPTest : MonoBehaviour
{
    public AnimatedTMP animatedTMP;
    public Dialogue dialogue;

    private int messageIndex;

    private void Start() {
        messageIndex = -1;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (animatedTMP.IsTextScrolling()){
                animatedTMP.ReadText(dialogue.messages[messageIndex]);
            } else if (messageIndex < dialogue.messages.Length - 1) {
                    messageIndex++;
                    animatedTMP.ReadText(dialogue.messages[messageIndex]);
            }
        }
    }
}
