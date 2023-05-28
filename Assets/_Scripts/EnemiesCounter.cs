using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesCounter : MonoBehaviour
{
    [SerializeField] private List<int> _totalEnemiesToKillPerWave;
    [SerializeField] private int _maxWavesToSpawn = 3;

    [BoxGroup("Listens to")]
    [SerializeField] private VoidEventChannelSO _enemyDeathChannel;
    [BoxGroup("Broadcast on")]
    [SerializeField] private IntSenderEventChannelSO _launchWaveChannel;
    [BoxGroup("Broadcast on")]
    [SerializeField] private VoidEventChannelSO _allWavesClearedChannel;

    private int _currentWaveIndex;
    private int _currentKilledEnemiesCount;

    private void Awake()
    {
        _currentWaveIndex = 0;
        _currentKilledEnemiesCount = 0;
    }

    private void OnEnable()
    {
        _enemyDeathChannel.OnEventTrigger += IncrementKilledEnemiesCount;
    }

    private void OnDisable()
    {
        _enemyDeathChannel.OnEventTrigger -= IncrementKilledEnemiesCount;
    }

    private void Start()
    {
        _launchWaveChannel.RequestRaiseEvent(_currentWaveIndex);
    }

    private void IncrementKilledEnemiesCount()
    {
        ++_currentKilledEnemiesCount;

        if (_currentKilledEnemiesCount >= _totalEnemiesToKillPerWave[_currentWaveIndex])
        {
            IncrementWaveIndex();
            _currentKilledEnemiesCount = 0;
            _launchWaveChannel.RequestRaiseEvent(_currentWaveIndex);
        }
    }

    private void IncrementWaveIndex()
    {
        ++_currentWaveIndex;
        if (_currentWaveIndex >= _maxWavesToSpawn)
        {
            _allWavesClearedChannel.RequestRaiseEvent();
        }
    }
}