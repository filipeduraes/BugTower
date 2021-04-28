using BugTower.Patterns;
using System.Collections;
using UnityEngine;

namespace BugTower.Player
{
    [RequireComponent(typeof(PlayerController), typeof(Rigidbody2D))]
    public class PlayerHealth : Health
    {
        [SerializeField] private Animator playerAnimator;
        private PlayerController controller;
        private Rigidbody2D playerRigidbody;
        private Vector2 spawnPosition;
        private bool playerIsRolled;

        protected override void Awake()
        {
            base.Awake();
            spawnPosition = transform.position;

            controller = GetComponent<PlayerController>();
            playerRigidbody = GetComponent<Rigidbody2D>();

            controller.OnStateChange += Controller_OnStateChange;
        }

        private void Controller_OnStateChange(State state)
        {
            playerIsRolled = state is PlayerRolledState;
        }

        public override void TakeDamage(float damage)
        {
            if (!playerIsRolled)
            {
                base.TakeDamage(damage);
                return;
            }

            float x = Random.Range(20, 50);
            float y = Random.Range(20, 50);

            int xModifier = Random.Range(-1, 1);
            int yModifier = Random.Range(-1, 1);

            Vector2 atackForce = new Vector2(x * xModifier, y * yModifier);
            playerRigidbody.AddForce(atackForce);
        }

        protected override void Death()
        {
            playerAnimator.SetTrigger("death");
            float duration = playerAnimator.GetCurrentAnimatorStateInfo(0).length;
            StartCoroutine(RespawnAfterTime(duration));
        }

        private IEnumerator RespawnAfterTime(float time)
        {
            controller.enabled = false;

            yield return new WaitForSeconds(time);

            controller.enabled = true;
            transform.position = spawnPosition;
            RestoreToFullHealth();
        }
    }
}
