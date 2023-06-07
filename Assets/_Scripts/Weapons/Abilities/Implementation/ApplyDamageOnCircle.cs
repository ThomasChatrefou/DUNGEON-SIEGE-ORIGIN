using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ApplyDamageOnCircle", menuName = "ScriptableObjects/Weapons/Abilities/ApplyDamageOnCircle")]
public class ApplyDamageOnCircle : BaseAbilitySO
{
    [Layer]
    [Label("Layer for hitable objects")]
    [SerializeField] private string hitableLayerName = "Hitable";

    [SerializeField] private int _circleAccuracy = 10;

    public override void Use(ref AbilityBlackboard abilityData)
    {
        int layer = 1 << LayerMask.NameToLayer(hitableLayerName);
        if (CircleCaster.CircleCast(out List<Collider> hitColliders, abilityData.Caster.position, Vector3.up, abilityData.Range, _circleAccuracy, layer))
        {
            foreach (Collider collider in hitColliders)
            {
                ICharacterHealth healthComponent = collider.gameObject.GetComponent<ICharacterHealth>();
                healthComponent?.TakeDamage((int)abilityData.Damages);
            }
        }
    }
}
