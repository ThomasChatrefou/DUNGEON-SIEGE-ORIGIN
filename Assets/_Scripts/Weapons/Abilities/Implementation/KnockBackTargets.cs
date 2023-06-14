using UnityEngine;

[CreateAssetMenu(fileName = "KnockBackTargets", menuName = "ScriptableObjects/Weapons/Abilities/KnockBackTargets")]
public class KnockBackTargets : BaseAbilitySO
{
    public override void Use(ref AbilityBlackboard abilityData)
    {
        Debug.Log("Knocking back");
    }
}