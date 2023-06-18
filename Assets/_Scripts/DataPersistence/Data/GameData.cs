using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int currentLevel;

    public byte characterID;

    public byte weaponID;

    public Dictionary<byte, int> weaponUpgrade = new Dictionary<byte, int>();

    /// <summary>
    /// Constructor
    /// </summary>
    public GameData()
    {
        currentLevel = 0;
        characterID = 255;
        weaponID = 255;
        for (byte i = 0; i < Byte.MaxValue; i++)
        {
            weaponUpgrade[i] = 0;
        }
    }
}
