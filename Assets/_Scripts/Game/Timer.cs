using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [BoxGroup("Listen To")]
    [SerializeField] private FloatSenderEventChannelSO _putTimerValue;

    private float _timeBeforeNextWave;
    private Coroutine _runningCoroutine;

    private void OnEnable()
    {
        _putTimerValue.OnEventTrigger += StartTimerCoroutine;
    }

    private void OnDisable()
    {
        _putTimerValue.OnEventTrigger -= StartTimerCoroutine;
    }

    private void StartTimerCoroutine(float timeBeforeNextWave)
    {
        if(_runningCoroutine != null )
            StopTimerCoroutine();

        _timeBeforeNextWave = timeBeforeNextWave;
        _runningCoroutine = StartCoroutine(TimerBeforeNextWave());
    }

    private void StopTimerCoroutine()
    {
        StopCoroutine(_runningCoroutine);
    }

    private IEnumerator TimerBeforeNextWave()
    {
        while (_timeBeforeNextWave >= 0)
        {
            Debug.Log("Temps restant : " + _timeBeforeNextWave);
            yield return new WaitForSeconds(1f);
            _timeBeforeNextWave -= 1f;
        }
    }
}
