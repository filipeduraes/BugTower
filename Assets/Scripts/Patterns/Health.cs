using UnityEngine;

namespace BugTower.Patterns
{
    public abstract class Health : MonoBehaviour
    {
        [SerializeField] protected float maxHealth = 1f;
        protected float currentHealth;

        protected virtual void Awake()
        {
            RestoreToFullHealth();
        }

        public virtual void TakeDamage(float damage)
        {
            currentHealth -= damage;

            if (currentHealth <= 0f)
                Death();
        }

        public virtual void RestoreToFullHealth()
        {
            currentHealth = maxHealth;
        }

        public virtual void RestoreHealth(float health)
        {
            float newHealth = currentHealth + health;

            if (newHealth <= maxHealth)
                currentHealth = newHealth;
        }

        protected abstract void Death();
    }
}
