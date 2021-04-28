using UnityEngine;
using UnityEngine.Events;

namespace BugTower.Miscellaneous
{
    public class DoActionOnStart : MonoBehaviour
    {
        [SerializeField] private UnityEvent action;

        private void Start() => action?.Invoke();

        public void DestroyObject() => Destroy(gameObject);
    }
}