using UnityEngine;
using UnityEngine.Events;

namespace BugTower.Miscellaneous
{
    public class DoActionOnTrigger : MonoBehaviour
    {
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private UnityEvent action;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!playerLayer.CompareLayer(collision.gameObject.layer))
                return;

            action?.Invoke();
        }

        public void DestroyObject() => Destroy(gameObject);
    }
}
