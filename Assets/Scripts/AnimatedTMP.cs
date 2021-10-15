using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TMPro {
    // Custom parameter UnityEvent types
    [System.Serializable] public class DialogueEvent : UnityEvent {}
    [System.Serializable] public class TextScrollEvent : UnityEvent<string> {}
    [System.Serializable] public class ActionEvent : UnityEvent<string> {}
    

    /// <summary>
    /// Implementation of an animated textbox with scrolling text
    /// Heavily inspired by the Mix and Jam implementation: https://github.com/mixandjam/AC-Dialogue/blob/master/Assets/TMP_Animated/Runtime/TMP_Animated.cs
    /// </summary>
    public class AnimatedTMP : TextMeshProUGUI
    {
        // Events
        public DialogueEvent onDialogueStart;
        public DialogueEvent onDialogueFinish;
        public TextScrollEvent onTextScroll;
        
        // Exposed fields
        [SerializeField] private float characterDelay = 0.1f;

        // Instance variables
        private Coroutine activeTextCoroutine;
        private bool isTextScrolling;

        protected override void Start() {
            text = string.Empty;
        }

        public void InterruptText(bool skipToEnd, string newText) {
            StopCoroutine(activeTextCoroutine);
            isTextScrolling = false;

            if (skipToEnd) {
                text = newText;
            }

            onDialogueFinish.Invoke();
        }

        public bool IsTextScrolling() {
            return isTextScrolling;
        }

        public void ReadText(string newText) {
            if (!isTextScrolling) {
                text = string.Empty;
                activeTextCoroutine = StartCoroutine(ScrollTextToEnd(newText));
            } else {
                InterruptText(true, newText);
            }
        }

        private IEnumerator ScrollTextToEnd(string newText) {
            int charIndex = 0;
            isTextScrolling = true;
            onDialogueStart.Invoke();
            
            while (charIndex < newText.Length) {
                // Wait delay
                yield return new WaitForSeconds(characterDelay);

                charIndex++;

                /* Add whole ass tag at once if there is a tag here please
                * Note: I am going to break it if there is an incomplete tag
                */ 
                if (newText[charIndex-1] == '<') {
                    while (newText[charIndex-1] != '>') {
                        charIndex++;
                    }
                }

                text = newText.Substring(0, charIndex);
                onTextScroll.Invoke(newText.Substring(charIndex-1));
            }
            
            isTextScrolling = false;
            onDialogueFinish.Invoke();
        }

        
    }
}