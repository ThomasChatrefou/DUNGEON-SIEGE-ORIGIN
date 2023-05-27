using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    [SerializeField] private SpawnerDataSO spawnerDataSO;
    [SerializeField] private IntSenderEventChannelSO launchWaveChannel;

    private void OnEnable()
    {
        launchWaveChannel.OnEventTrigger += OnLaunchWaveEvent;
    }

    private void OnDisable()
    {
        launchWaveChannel.OnEventTrigger -= OnLaunchWaveEvent;
    }

    private void OnLaunchWaveEvent(int index)
    {
        if (spawnerDataSO.Waves.Count <= index)
            return;

        if (spawnerDataSO.Waves[index].DoSpawn)
        {
            SpawnEnemy(spawnerDataSO.Waves[index].Prefab);
        }
    }

    private void SpawnEnemy(GameObject prefab)
    {
        Instantiate(prefab, transform);
        //obj.GetComponent<KiterController>().Target = player;
    }

}
