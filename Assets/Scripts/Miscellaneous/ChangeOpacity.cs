using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BugTower.Miscellaneous
{
    [RequireComponent(typeof(Image))]
    public class ChangeOpacity : MonoBehaviour
    {
        [SerializeField] private float finalAlpha;
        [SerializeField] private float seconds;
        [SerializeField] private UnityEvent OnChangingEnd;

        private Image image;
        private float initialAlpha = 1f;
        private bool isLerping = false;

        private void Awake()
        {
            image = GetComponent<Image>();
            initialAlpha = image.color.a;
        }

        public void LerpToFinalAlpha()
        {
            if(!isLerping)
                StartCoroutine(LerpAlpha(initialAlpha, finalAlpha));
        }

        public void LerpToInitialAlpha()
        {
            if(!isLerping)
                StartCoroutine(LerpAlpha(finalAlpha, initialAlpha));
        }

        private IEnumerator LerpAlpha(float initial, float final)
        {
            isLerping = true;
            float timer = 0f;

            while(initial != final)
            {
                Color color = image.color;
                color.a = Mathf.Lerp(initial, final, timer / seconds);
                image.color = color;

                timer += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            OnChangingEnd?.Invoke();
            isLerping = false;
        }
    }
}