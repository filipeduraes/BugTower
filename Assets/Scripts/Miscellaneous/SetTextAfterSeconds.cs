using System.Collections;
using TMPro;
using UnityEngine;

namespace BugTower.Miscellaneous
{
    public class SetTextAfterSeconds : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMeshPro = null;

        [Header("Appear")]
        [SerializeField] private float seconds = 2f;
        [SerializeField] private float smoothTime = 1f;

        [Header("Disappear")]
        [SerializeField] private bool disappearAfterTime = true;
        [SerializeField] private float showingTime = 5f;

        private bool textCanAppear = true;

        public void StartShowingText(string text)
        {
            if (!textCanAppear)
                return;

            StartCoroutine(ControlText(seconds, text));
        }

        private IEnumerator ControlText(float seconds, string text)
        {
            textCanAppear = false;
            yield return HideText();

            yield return new WaitForSeconds(seconds);

            textMeshPro.SetText(text);
            yield return ShowText();

            yield return new WaitForSeconds(showingTime);

            if (disappearAfterTime)
                yield return HideText();

            textCanAppear = true;
        }

        private IEnumerator HideText()
        {
            float timer = 0f;
            float initialAlpha = textMeshPro.color.a;

            while (textMeshPro.color.a > 0f)
            {
                Color color = textMeshPro.color;
                color.a = Mathf.Lerp(initialAlpha, 0f, timer / smoothTime);
                textMeshPro.color = color;
                timer += Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }
        }

        private IEnumerator ShowText()
        {
            float timer = 0f;
            float initialAlpha = textMeshPro.color.a;

            while (textMeshPro.color.a < 1f)
            {
                Color color = textMeshPro.color;
                color.a = Mathf.Lerp(initialAlpha, 1f, timer / smoothTime);
                textMeshPro.color = color;
                timer += Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }
        }
    }
}