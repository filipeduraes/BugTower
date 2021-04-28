using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{
    [SerializeField] private int[] levelsToPlay;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        bool dontDestroy = false;

        foreach(int index in levelsToPlay)
            dontDestroy |= arg1.buildIndex == index;

        if (!dontDestroy)
            Destroy(gameObject);
    }
}
