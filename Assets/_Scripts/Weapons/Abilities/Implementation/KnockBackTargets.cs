using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "KnockBackTargets", menuName = "ScriptableObjects/Weapons/Abilities/KnockBackTargets")]
public class KnockBackTargets : BaseAbilitySO
{
    public override void Use(ref AbilityBlackboard abilityData)
    {
        foreach (Transform target in abilityData.Targets)
        {
            target.GetComponent<Knockback>().DoKnockback(
                (target.position - abilityData.Caster.position).normalized, 5.0f
            );
        }
    }

}