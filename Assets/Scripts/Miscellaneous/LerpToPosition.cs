using System.Collections;
using UnityEngine;

namespace BugTower.Miscellaneous
{
    public class LerpToPosition : MonoBehaviour
    {
        [SerializeField] private Vector2 finalPosition;
        [SerializeField] private float totalTime;
        private bool movementEnded = true;

        public void StartLerping()
        {
            if (movementEnded)
                StartCoroutine(Lerp(transform.localPosition));
        }

        private IEnumerator Lerp(Vector2 startPosition)
        {
            movementEnded = false;
            float elapsedTime = 0;

            while(elapsedTime < totalTime)
            {
                transform.localPosition = Vector3.Lerp(startPosition, finalPosition, elapsedTime / totalTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            movementEnded = true;
        }
    }
}