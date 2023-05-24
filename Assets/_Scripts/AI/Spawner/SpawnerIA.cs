using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerIA : MonoBehaviour
{
    [SerializeField] SpawnerSO spawnerList;
    [SerializeField] float time = 10;
    [SerializeField] int pack = 5;

    List<GameObject> spawner;
    public bool PartyOn;

    // Start is called before the first frame update
    void Start()
    {
        PartyOn = true;

        StartCoroutine(EndOfGame());
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(time);
        SpawnIA();
    }

    IEnumerator EndOfGame()
    {
        yield return new WaitForSeconds(time);
        PartyOn = false;
    }

    private void SpawnIA()
    {
        if (spawnerList != null)
        {
            for (int i = 0; i < spawnerList.; i++)
        }
    }
}
