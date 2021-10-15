using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnimatedTMPTest : MonoBehaviour
{
    public AnimatedTMP animatedTMP;
    public Dialogue dialogue;

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            animatedTMP.ReadText(dialogue.messages[0]);
        }
    }
}
