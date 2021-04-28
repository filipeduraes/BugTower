using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace BugTower.NPCs.Dialogue
{
    public class Dialogue : MonoBehaviour
    {
        private const string MARK_X = "x";
        private const string MARK_EXCLAMATION = "!";

        [SerializeField] private TextMeshPro dialogueMark;
        [SerializeField] private TextMeshProUGUI dialogueBox;
        [SerializeField] private DialogueSentences sentences;
        [SerializeField] private LayerMask playerLayer;
        [Range(0f, 1f), SerializeField] private float textSpeed;
        [SerializeField] private UnityEvent onDialogueEnded;

        private readonly DialogueInput input = new DialogueInput();
        private readonly DialogueTextEditor editor = new DialogueTextEditor();
        private DialogueMachine machine;
        private DialogueExibition exibition;

        private bool isOnTrigger = false;

        private void Awake()
        {
            machine = new DialogueMachine(sentences.Sentences);
            exibition = new DialogueExibition(dialogueBox, this, editor, machine);
            editor.SetTextMeshProUGUI(dialogueBox, string.Empty);
        }

        private void OnEnable()
        {
            machine.OnDialogueEnded += Machine_OnDialogueEnded;
        }

        private void OnDisable()
        {
            machine.OnDialogueEnded -= Machine_OnDialogueEnded;
        }

        private void Machine_OnDialogueEnded(object sender, System.EventArgs e)
        {
            editor.SetTextMeshPro(dialogueMark, string.Empty);
            editor.SetTextMeshProUGUI(dialogueBox, string.Empty);
            Destroy(this);
            onDialogueEnded?.Invoke();
        }

        private void Update()
        {
            input.RegisterInput();
            exibition.PlayCoroutineOnInput(input.OnXKeyPressed, isOnTrigger, textSpeed);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!playerLayer.CompareLayer(collision.gameObject.layer))
                return;

            isOnTrigger = true;
            editor.SetTextMeshPro(dialogueMark, MARK_X);
            exibition.EnteredTrigger();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!playerLayer.CompareLayer(collision.gameObject.layer))
                return;

            isOnTrigger = false;
            editor.SetTextMeshPro(dialogueMark, MARK_EXCLAMATION);
            exibition.ExitedTrigger();
        }
    }
}
