using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSO", menuName = "ScriptableObjects/Character")]
public class CharacterDataSO : ScriptableObject
{
    public float Health;
    public float HealthMax;
    public float Damages;
    public float MovementSpeed;
    public float AttackSpeed;
    public Mesh Mesh;
}
