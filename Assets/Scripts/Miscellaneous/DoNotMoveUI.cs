using UnityEngine;

namespace BugTower.Miscellaneous
{
    public class DoNotMoveUI : MonoBehaviour
    {
        private RectTransform rectTransform = null;
        private Vector3 position = Vector3.zero;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            position = rectTransform.position;
        }

        private void Update()
        {
            Vector3 currentPosition = rectTransform.position;

            if (currentPosition != position)
                rectTransform.position = position;
        }
    }
}