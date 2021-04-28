using BugTower.Patterns;
using UnityEngine;

namespace BugTower.NPCs.Dialogue
{
    public class DialogueInput : InputRegister
    {
        public bool OnXKeyPressed;

        public override void RegisterInput()
        {
            OnXKeyPressed = Input.GetKeyDown(KeyCode.X);
        }
    }
}
