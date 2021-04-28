using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DoActionOnTime : MonoBehaviour
{
    [SerializeField] private float seconds = 10f;
    [SerializeField] private UnityEvent OnTime;
    private bool timerHasStarted = false;

    public void StartTimer()
    {
        if (!timerHasStarted)
            StartCoroutine(InvokeAfterTime(seconds));
    }

    private IEnumerator InvokeAfterTime(float seconds)
    {
        timerHasStarted = true;
        yield return new WaitForSeconds(seconds);
        OnTime?.Invoke();
        timerHasStarted = false;
    }
}
