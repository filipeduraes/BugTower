using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace BugTower.Miscellaneous
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] private float duration;
        [SerializeField] private UnityEvent transition;

        private Coroutine currentOperation = null;

        public void LoadScene(int buildIndex)
        {
            if (currentOperation != null)
                return;

            currentOperation = StartCoroutine(LoadSceneWithTransition(buildIndex));
        }

        private IEnumerator LoadSceneWithTransition(int buildIndex)
        {
            transition.Invoke();
            yield return new WaitForSeconds(duration);
            SceneManager.LoadScene(buildIndex);
        }
    }
}