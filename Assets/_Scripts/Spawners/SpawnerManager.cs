using NaughtyAttributes;
using System.Collections;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private int maxWaveToSpawn = 3;
    [SerializeField] private float _timeBetweenWaves;

    [BoxGroup("Broadcast on")]
    [SerializeField] private IntSenderEventChannelSO _launchWaveChannel;

    private int _waveCount = 0;

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        while (_waveCount < maxWaveToSpawn)
        {
            yield return new WaitForSeconds(_timeBetweenWaves);
            _launchWaveChannel.RequestRaiseEvent(_waveCount);
            _waveCount++;
        }
    }
}
