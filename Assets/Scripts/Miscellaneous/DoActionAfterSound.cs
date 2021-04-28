using UnityEngine;
using UnityEngine.Events;

namespace BugTower.Miscellaneous
{
    [RequireComponent(typeof(AudioSource))]
    public class DoActionAfterSound : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnSoundPlayed;
        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (audioSource.clip.length != audioSource.time)
                return;

            OnSoundPlayed?.Invoke();
            Destroy(gameObject);
        }
    }
}