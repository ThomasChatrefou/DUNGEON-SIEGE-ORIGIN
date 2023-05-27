using NaughtyAttributes;
using UnityEngine;

public class EnemiesCounter : MonoBehaviour
{
    [SerializeField] private int totalEnemiesToKill;

    [BoxGroup("Listens to")]
    [SerializeField] private VoidEventChannelSO _enemyDeathChannel;
    [BoxGroup("Broadcast on")]
    [SerializeField] private VoidEventChannelSO _killThemAllChannel;

    private int currentKilledEnemiesCount = 0;

    private void OnEnable()
    {
        _enemyDeathChannel.OnEventTrigger += IncrementKilledEnemiesCount;
    }

    private void OnDisable()
    {
        _enemyDeathChannel.OnEventTrigger -= IncrementKilledEnemiesCount;
    }

    private void IncrementKilledEnemiesCount()
    {
        ++currentKilledEnemiesCount;
        if (currentKilledEnemiesCount >= totalEnemiesToKill)
        {
            _killThemAllChannel.RequestRaiseEvent();
        }
    }
}