using BugTower.Patterns;
using UnityEngine;

namespace BugTower.Player
{
    public class PlayerNormalState : State
    {
        private readonly PlayerController player;
        private readonly PlayerInput playerInput;
        private readonly PlayerMovement playerMovement;
        private readonly PlayerAnimation playerAnimation;
        private readonly float playerSpeed;

        public PlayerNormalState(PlayerController playerFSM, PlayerInput input, PlayerMovement movement, PlayerAnimation animation, float speed)
        {
            player = playerFSM;
            playerInput = input;
            playerMovement = movement;
            playerSpeed = speed;
            playerAnimation = animation;
        }

        public override void FixedUpdateAction()
        {
            playerMovement.MoveTo(playerInput.Axis * playerSpeed * Time.deltaTime);
        }

        public override void UpdateAction()
        {
            CheckForTransition();
            CheckForAnimation();
        }

        private void CheckForAnimation()
        {
            playerAnimation.IsMoving = playerInput.Axis.x != 0f;

            if (playerAnimation.IsMoving)
                playerAnimation.SetAxisAcordingToInput(playerInput);
        }

        private void CheckForTransition()
        {
            bool canTransitionToUp = playerInput.VerticalButtonDown;
            bool buttonChanged = (playerInput.HorizontalButtonUp && playerInput.VerticalButton);
            canTransitionToUp = canTransitionToUp || buttonChanged;

            if (canTransitionToUp)
                player.TransitionToState(player.UpState);

            if (playerInput.ZKeyIsPressed)
                player.TransitionToState(player.RolledState);
        }
    }
}