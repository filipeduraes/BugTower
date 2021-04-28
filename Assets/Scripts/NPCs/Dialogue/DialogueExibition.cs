using System.Collections;
using TMPro;
using UnityEngine;

namespace BugTower.NPCs.Dialogue
{
    public class DialogueExibition
    {
        private readonly TextMeshProUGUI dialogueTextMesh;
        private readonly MonoBehaviour dialogueScript;
        private readonly DialogueTextEditor textEditor;
        private readonly DialogueMachine dialogueMachine;
        private Coroutine currentTextCoroutine;
        private string currentTextBeingDisplayed = string.Empty;
        private bool textHasBeenDisplayed = true;
        private bool dialogueHasStarted = false;

        public DialogueExibition(TextMeshProUGUI dialogueTmPro, MonoBehaviour dialogue, DialogueTextEditor editor, DialogueMachine machine)
        {
            dialogueTextMesh = dialogueTmPro;
            dialogueScript = dialogue;
            textEditor = editor;
            dialogueMachine = machine;
        }

        public void PlayCoroutineOnInput(bool input, bool isTriggered, float textSpeed)
        {
            if (!(input && isTriggered))
                return;

            dialogueHasStarted.SetWithoutOverwrite(true);

            if (textHasBeenDisplayed)
            {
                currentTextBeingDisplayed = dialogueMachine.GetFromQueue();
                currentTextCoroutine = dialogueScript.StartCoroutine(ShowText(currentTextBeingDisplayed, textSpeed));
                return;
            }

            dialogueScript.StopCoroutine(currentTextCoroutine);
            textEditor.SetTextMeshProUGUI(dialogueTextMesh, currentTextBeingDisplayed);
            textHasBeenDisplayed = true;
        }

        public void EnteredTrigger()
        {
            if (!dialogueHasStarted)
                return;

            textEditor.SetTextMeshProUGUI(dialogueTextMesh, currentTextBeingDisplayed);
        }

        public void ExitedTrigger()
        {
            if (!dialogueHasStarted)
                return;

            dialogueScript.StopCoroutine(currentTextCoroutine);
            textEditor.SetTextMeshProUGUI(dialogueTextMesh, string.Empty);
            textHasBeenDisplayed.SetWithoutOverwrite(true);
        }

        private IEnumerator ShowText(string text, float secondsToWait)
        {
            textHasBeenDisplayed = false;
            string newText = string.Empty;

            for (int index = 0; index < text.Length; index++)
            {
                newText += text[index];
                textEditor.SetTextMeshProUGUI(dialogueTextMesh, newText);
                yield return new WaitForSeconds(secondsToWait);
            }

            textHasBeenDisplayed = true;
        }
    }
}