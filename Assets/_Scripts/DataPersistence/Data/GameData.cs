using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public bool isInRun;

    public int currentLevel;

    public byte playerID;

    public byte weaponID;

    /// <summary>
    /// Constructor
    /// </summary>
    public GameData()
    {
        isInRun = false;
        currentLevel = 0;
        playerID = 255;
        weaponID = 255;
    }
}
