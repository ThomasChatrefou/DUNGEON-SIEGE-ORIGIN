using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "FindNearestTarget", menuName = "ScriptableObjects/Weapons/Abilities/FindNearestTarget")]
public class FindNearestTarget : BaseAbilitySO
{
    [Layer]
    [Label("Layer for hitable objects")]
    [SerializeField] private string _targetLayerName = "Hitable";
    [SerializeField] private float _maxRange = 10.0f;

    public override void Use(ref AbilityBlackboard abilityData)
    {
        Vector3 origin = abilityData.Caster.position;
        int layer = 1 << LayerMask.NameToLayer(_targetLayerName);
        Collider[] detectedTargets = Physics.OverlapSphere(origin, _maxRange, layer);

        float minDistance = Mathf.Infinity;
        int minIndex = -1;
        for (int i = 0; i < detectedTargets.Length; ++i)
        {
            float distanceToCurrentEnemy = Vector3.Distance(origin, detectedTargets[i].transform.position);
            if (distanceToCurrentEnemy < minDistance)
            {
                minDistance = distanceToCurrentEnemy;
                minIndex = i;
            }
        }

        if (minIndex > -1)
        {
            abilityData.Target = detectedTargets[minIndex].transform;
        }

        Debug.Log("ABILITIES : FindNearestTarget | " + abilityData.Target);
    }
}
