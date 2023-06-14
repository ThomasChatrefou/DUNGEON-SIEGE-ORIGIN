using UnityEngine;

[CreateAssetMenu(fileName = "KnockBackTargets", menuName = "ScriptableObjects/Weapons/Abilities/KnockBackTargets")]
public class KnockBackTargets : BaseAbilitySO
{
    public override void Use(ref AbilityBlackboard abilityData)
    {
        foreach (Transform target in abilityData.Targets)
        {
            if (target.TryGetComponent(out Knockback knockbackComponent))
            {
                knockbackComponent.DoKnockback(
                    (target.position - abilityData.Caster.position).normalized, 5.0f
                );
            }
        }
    }

}