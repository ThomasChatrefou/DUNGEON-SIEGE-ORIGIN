using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject prefabToSpawn;
    [SerializeField] Transform player;

    public void SpawnEnemy()
    {
        GameObject obj = Instantiate(prefabToSpawn, transform);
        obj.GetComponent<KiterController>().Target = player;
    }
}
