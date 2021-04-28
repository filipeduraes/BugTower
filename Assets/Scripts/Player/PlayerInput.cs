using BugTower.Patterns;
using System;
using UnityEngine;

namespace BugTower.Player
{
    public class PlayerInput : InputRegister
    {
        public Vector2 Axis { get; private set; }
        public bool ZKeyIsPressed { get; private set; }
        public bool ZKeyReleased { get; private set; }

        #region Axis Buttons
        public bool HorizontalButtonDown { get; private set; }
        public bool VerticalButtonDown { get; private set; }
        public bool HorizontalButtonUp { get; private set; }
        public bool VerticalButtonUp { get; private set; }
        public bool HorizontalButton { get; private set; }
        public bool VerticalButton { get; private set; }
        #endregion

        public override void RegisterInput()
        {
            float horizontalAxis = Input.GetAxisRaw(HORIZONTAL_INPUT);
            float verticalAxis = Input.GetAxisRaw(VERTICAL_INPUT);

            Axis = new Vector2(horizontalAxis, verticalAxis);
            RegisterAxisButtons();

            ZKeyIsPressed = Input.GetKey(KeyCode.Z);
            ZKeyReleased = Input.GetKeyUp(KeyCode.Z);
        }

        private void RegisterAxisButtons()
        {
            HorizontalButtonDown = Input.GetButtonDown(HORIZONTAL_INPUT);
            VerticalButtonDown = Input.GetButtonDown(VERTICAL_INPUT);

            HorizontalButtonUp = Input.GetButtonUp(HORIZONTAL_INPUT);
            VerticalButtonUp = Input.GetButtonUp(VERTICAL_INPUT);

            HorizontalButton = Input.GetButton(HORIZONTAL_INPUT);
            VerticalButton = Input.GetButton(VERTICAL_INPUT);
        }
    }
}