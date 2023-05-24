using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSO", menuName = "ScriptableObjects/Character")]
public class CharacterSO : ScriptableObject
{
    public float health;
    public float healthMax;
    public float damages;
    public float movementSpeed;
    public float attackSpeed;
    public Mesh mesh;
    public GameObject weapon;
}
