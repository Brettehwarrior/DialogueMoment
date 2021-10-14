using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


// Hey Trent check this out this is what you want probably: https://github.com/mixandjam/AC-Dialogue/blob/master/Assets/TMP_Animated/Runtime/TMP_Animated.cs

public class ScrollText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    [TextArea(15,20)]
    private string[] messages;


    [SerializeField]
    private float characterDelay = 0.1f;

    private int messageIndex;
    private int charIndex;

    private Coroutine activeTextCoroutine;

    private void Start() {
        messageIndex = 0;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0))
            NextMessage();
    }

    /// <summary>
    /// Advance to next message I guess
    /// </summary>
    public void NextMessage() {
        if (messageIndex >= messages.Length - 1)
            return;

        charIndex = 0;
        
        if (activeTextCoroutine != null) {
            StopCoroutine(activeTextCoroutine);
            messageIndex++;
        }
        
        activeTextCoroutine = StartCoroutine("AdvanceText");
    }

    /// <summary>
    /// Set the displayed text of the TextMeshPro
    /// </summary>
    /// <param name="newText">New text</param>
    public void SetText(string newText) {
        text.text = newText;
    }


    private IEnumerator AdvanceText() {
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
    }
}
