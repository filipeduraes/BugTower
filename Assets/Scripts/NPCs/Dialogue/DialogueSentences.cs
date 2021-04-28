using UnityEngine;

namespace BugTower.NPCs.Dialogue
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "BugTower/NPCs/Dialogue", order = 0)]
    public class DialogueSentences : ScriptableObject
    {
        public string[] Sentences => sentences;
        [SerializeField] private string[] sentences;
    }
}
