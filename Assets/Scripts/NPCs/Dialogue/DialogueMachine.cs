using System;
using System.Collections.Generic;

namespace BugTower.NPCs.Dialogue
{
    public class DialogueMachine
    {
        public event EventHandler OnDialogueEnded;
        private readonly Queue<string> dialogueQueue = new Queue<string>();

        public DialogueMachine(string[] sentences)
        {
            for(int i = 0; i < sentences.Length; i++)
                AddToQueue(sentences[i]);
        }

        public string GetFromQueue() 
        {
            if (dialogueQueue.Count != 0)
                return dialogueQueue.Dequeue();

            OnDialogueEnded?.Invoke(this, EventArgs.Empty);
            return string.Empty;
        }

        public void AddToQueue(string text) => dialogueQueue.Enqueue(text);
    }
}