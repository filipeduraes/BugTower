using BugTower.Patterns;
using System.Collections;
using UnityEngine;

namespace BugTower.NPCs.Enemy
{
    public class FighterHealth : Health
    {
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private Color damageColor = Color.red;

        private Color initialColor;
        private bool damageAnimationIsPlaying = false;

        protected override void Awake()
        {
            base.Awake();
            initialColor = sprite.color;
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);

            if (!damageAnimationIsPlaying && gameObject.activeSelf)
                StartCoroutine(PlayDamageAnimation(0.5f));
        }

        protected override void Death()
        {
            gameObject.SetActive(false);
        }

        private IEnumerator PlayDamageAnimation(float duration)
        {
            damageAnimationIsPlaying = true;
            sprite.color = damageColor;

            yield return new WaitForSeconds(duration);

            sprite.color = initialColor;
            damageAnimationIsPlaying = false;
        }
    }
}
