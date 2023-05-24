using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //In Game save data

    //In Run save data
    public bool isInRun;

    /// <summary>
    /// Constructor
    /// </summary>
    public GameData()
    {
        isInRun = false;
    }
}
