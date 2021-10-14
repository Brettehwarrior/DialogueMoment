using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


// Hey Trent check this out this is what you want probably: https://github.com/mixandjam/AC-Dialogue/blob/master/Assets/TMP_Animated/Runtime/TMP_Animated.cs

[RequireComponent(typeof(AudioSource))]
public class ScrollText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    [TextArea(4,4)]
    private string[] messages;


    [SerializeField]
    private float characterDelay = 0.1f;

    [Header("More features")]
    [SerializeField]
    private DialogueAudio dialogueAudio;

    private int messageIndex;
    private int charIndex;

    private Coroutine activeTextCoroutine;
    private bool isTextScrolling;


    private void Start() {
        messageIndex = -1; // Always advances before using
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0))
            NextMessage();
    }



    /// <summary>
    /// Advance to next message I guess
    /// </summary>
    public void NextMessage() {
        if (isTextScrolling) {
            // Interrupt scrolling
            SetText(messages[messageIndex]);
            // charIndex = messages[messageIndex].Length;
            StopAdvancing();
        } else if (messageIndex < messages.Length - 1) {
            // Start coroutines
            charIndex = 0;
            
            messageIndex++;
            activeTextCoroutine = StartCoroutine("AdvanceText");

            if (dialogueAudio != null)
                dialogueAudio.StartSounds();
            
        }
        
    }

    /// <summary>
    /// Set the displayed text of the TextMeshPro
    /// </summary>
    /// <param name="newText">New text</param>
    public void SetText(string newText) {
        text.text = newText;
    }

    public void StopAdvancing() {
        isTextScrolling = false;
        StopCoroutine(activeTextCoroutine);

        if (dialogueAudio != null)
            dialogueAudio.StopSounds();
    }


    private IEnumerator AdvanceText() {
        
        isTextScrolling = true;
        
        while (charIndex < messages[messageIndex].Length) {
            
            // Wait delay
            yield return new WaitForSeconds(characterDelay);

            charIndex++;

            /* Add whole ass tag at once if there is a tag here please
             * Note: I am going to break it if there is an incomplete tag
             */ 
            if (messages[messageIndex][charIndex-1] == '<') {
                while (messages[messageIndex][charIndex-1] != '>') {
                    charIndex++;
                }
            }

            SetText(messages[messageIndex].Substring(0, charIndex));
        }
        
        StopAdvancing();
    }
}
