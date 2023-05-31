using NaughtyAttributes;
using System.Collections;
using UnityEngine;

public class PeriodicWaveLauncher : MonoBehaviour
{
    [SerializeField] private int _nbWavesToSpawn = 3;
    [SerializeField] private float _timeBetweenWaves;

    [BoxGroup("Broadcast on")]
    [SerializeField] private IntSenderEventChannelSO _launchWaveChannel;
    [BoxGroup("Broadcast on")]
    [SerializeField] private VoidEventChannelSO _endOfSpawnChannel;

    private int _currentWaveIndex = 0;

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        while (_currentWaveIndex < _nbWavesToSpawn)
        {
            yield return new WaitForSeconds(_timeBetweenWaves);
            _launchWaveChannel.RequestRaiseEvent(_currentWaveIndex);
            _currentWaveIndex++;
        }
        _endOfSpawnChannel.RequestRaiseEvent();
    }
}
