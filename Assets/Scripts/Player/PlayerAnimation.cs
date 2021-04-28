using UnityEngine;

namespace BugTower.Player
{
    public class PlayerAnimation
    {
        private readonly Animator playerAnimator = null;
        private readonly SpriteRenderer playerRenderer = null;

        private bool isRolled = false;
        public bool IsRolled
        {
            get => isRolled;
            set
            {
                playerAnimator.SetBool("isRolled", value);
                isRolled = value;
            }
        }

        private bool isMoving = false;
        public bool IsMoving
        {
            get => isMoving;
            set
            {
                playerAnimator.SetBool("isMoving", value);
                isMoving = value;
            }
        }

        private float horizontal = 0f;
        public float Horizontal
        {
            get => horizontal;
            set
            {
                playerAnimator.SetFloat("horizontal", value);
                horizontal = value;
            }
        }

        private float vertical = 0f;
        public float Vertical
        {
            get => vertical;
            set
            {
                playerAnimator.SetFloat("vertical", value);
                vertical = value;
            }
        }

        public PlayerAnimation(Animator animator, SpriteRenderer renderer)
        {
            playerAnimator = animator;
            playerRenderer = renderer;
        }

        public void SetAxisAcordingToInput(PlayerInput input)
        {
            Horizontal = Mathf.Abs(input.Axis.x);
            Vertical = Mathf.Abs(input.Axis.y);

            if (Vertical.Different(0f) && !Horizontal.Different(0f))
            {
                playerRenderer.flipY = input.Axis.y < 0f;
                playerRenderer.flipX = false;
            }
            else if (Horizontal.Different(0f))
            {
                playerRenderer.flipX = input.Axis.x < 0f;
                playerRenderer.flipY = false;
            }
        }
    }
}