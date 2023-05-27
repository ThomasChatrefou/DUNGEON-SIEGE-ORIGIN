using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Spawn/SpawnerData", fileName = "SpawnerData")]
public class SpawnerDataSO : ScriptableObject
{
    public List<WavePrefabToSpawn> Waves = new();
}

[Serializable]
public struct WavePrefabToSpawn
{
    public bool DoSpawn;
    [EnableIf("DoSpawn")]
    [AllowNesting]
    public GameObject Prefab;
}