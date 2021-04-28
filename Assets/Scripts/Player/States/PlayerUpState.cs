using BugTower.Patterns;
using UnityEngine;

namespace BugTower.Player
{
    public class PlayerUpState : State
    {
        private readonly PlayerController player;
        private readonly PlayerInput playerInput;
        private readonly PlayerMovement playerMovement;
        private readonly PlayerAnimation playerAnimation;
        private readonly float playerSpeed;

        public PlayerUpState(PlayerController playerFSM, PlayerInput input, PlayerMovement movement, PlayerAnimation animation, float speed)
        {
            player = playerFSM;
            playerInput = input;
            playerMovement = movement;
            playerAnimation = animation;
            playerSpeed = speed;
        }

        public override void UpdateAction()
        {
            CheckForTransition();
            CheckForAnimation();
        }

        private void CheckForAnimation()
        {
            playerAnimation.IsMoving = playerInput.Axis.y != 0f;

            if (playerAnimation.IsMoving)
                playerAnimation.SetAxisAcordingToInput(playerInput);
        }

        private void CheckForTransition()
        {
            bool canTransitionToNormal = playerInput.HorizontalButtonDown;
            bool buttonChanged = (playerInput.VerticalButtonUp && playerInput.HorizontalButton);
            canTransitionToNormal = canTransitionToNormal || buttonChanged;

            if (canTransitionToNormal)
                player.TransitionToState(player.NormalState);

            if (playerInput.ZKeyIsPressed)
                player.TransitionToState(player.RolledState);
        }

        public override void FixedUpdateAction()
        {
            playerMovement.MoveTo(playerInput.Axis * playerSpeed * Time.deltaTime);
        }
    }
}