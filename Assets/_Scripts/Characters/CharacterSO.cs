using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSO", menuName = "ScriptableObjects/Character")]
public class CharacterSO : ScriptableObject
{
    public float Health;
    public float HealthMax;
    public float Damages;
    public float MovementSpeed;
    public float AttackSpeed;
    public Mesh Mesh;
    public GameObject Weapon;
}
