using System.Collections;
using UnityEngine;

namespace BugTower.Miscellaneous
{
    [RequireComponent(typeof(RectTransform))]
    public class LerpToRectPosition : MonoBehaviour
    {
        [SerializeField] private Vector2 finalPosition;
        [SerializeField] private float totalTime;

        private bool movementEnded = true;
        private RectTransform rectTransform;
        private Vector3 startPosition;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            startPosition = rectTransform.localPosition;
        }

        public void StartLerping()
        {
            if (movementEnded)
                StartCoroutine(Lerp(startPosition, finalPosition));
        }

        public void LerpToStart()
        {
            if(movementEnded)
                StartCoroutine(Lerp(finalPosition, startPosition));
        }

        private IEnumerator Lerp(Vector2 startPosition, Vector2 finalPosition)
        {
            movementEnded = false;
            float elapsedTime = 0;

            while (elapsedTime < totalTime)
            {
                rectTransform.localPosition = Vector3.Lerp(startPosition, finalPosition, elapsedTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            movementEnded = true;
        }
    }
}