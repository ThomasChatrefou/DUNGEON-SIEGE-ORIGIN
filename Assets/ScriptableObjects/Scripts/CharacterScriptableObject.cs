using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterScriptableObject", menuName = "ScriptableObjects/CharacterScriptableObject")]
public class CharacterScriptableObject : ScriptableObject
{
    public float health;
    public float healthMax;
    public float damages;
    public float movementSpeed;
    public float attackSpeed;
    public Mesh mesh;
    public GameObject weapon;
}
