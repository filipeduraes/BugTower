using UnityEngine;

namespace BugTower.NPCs.Enemy
{
    public class EnemyAnimation
    {
        private readonly Animator enemyAnimator = null;
        private float xTimer = 0f;
        private float yTimer = 0f;

        private bool isMovingVerticaly = false;
        public bool IsMovingVerticaly
        {
            get => isMovingVerticaly;
            set
            {
                enemyAnimator.SetBool("isMovingVerticaly", value);
                isMovingVerticaly = value;
            }
        }

        private float vertical = 0f;
        public float Vertical
        {
            get => vertical;
            set
            {
                enemyAnimator.SetFloat("vertical", value);
                vertical = value;
            }
        }

        public EnemyAnimation(Animator animator)
        {
            enemyAnimator = animator;
        }

        public void CheckForAnimationChange(Vector2 velocity, float timeToTransition)
        {
            if (Mathf.Abs(velocity.x) > Mathf.Abs(velocity.y))
                xTimer += Time.deltaTime;
            else
                yTimer += Time.deltaTime;

            bool xTriggered = xTimer >= timeToTransition;
            bool yTriggered = yTimer >= timeToTransition;
            bool triggered = xTriggered || yTriggered;

            if (triggered)
            {
                IsMovingVerticaly = !xTriggered && yTriggered;

                xTimer = 0f;
                yTimer = 0f;
            }

            if(yTriggered)
                Vertical = Mathf.Round(velocity.normalized.y);
        }

        public void PlayAtackAnimation()
        {
            enemyAnimator.SetTrigger("atackTrigger");
        }
    }
}
