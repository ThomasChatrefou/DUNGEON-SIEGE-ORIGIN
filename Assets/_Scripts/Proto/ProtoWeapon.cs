using NaughtyAttributes;
using UnityEngine;
using System.Collections.Generic;

// [TODO] move this in another script
public interface IWeapon
{
    public void Use();
}

public class ProtoWeapon : MonoBehaviour, IWeapon
{
    [Layer]
    [Label("Layer for hitable objects")]
    [SerializeField] private string hitableLayerName = "Hitable";

    [SerializeField] private float damageZoneRadius = 1.0f;
    [SerializeField] private float damageZoneLifeTime = 0.4f;
    [SerializeField] private int damages = 1;
    [SerializeField] private GameObject damageZonePrefab;

    public void Use()
    {
        GameObject damageZoneGO = Instantiate(damageZonePrefab, transform.position, damageZonePrefab.transform.rotation);
        damageZoneGO.transform.localScale *= damageZoneRadius;
        Destroy(damageZoneGO, damageZoneLifeTime);

        int bitShiftedLayerMask = 1 << LayerMask.NameToLayer(hitableLayerName);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, damageZoneRadius, bitShiftedLayerMask);
        foreach (Collider collider in hitColliders)
        {
            ICharacterHealth healthComponent = collider.gameObject.GetComponent<ICharacterHealth>();
            if (healthComponent != null)
            {
                healthComponent.TakeDamage(damages);
            }
        }
    }
}

public class CircleCaster
{
    public static bool CircleCast(out List<RaycastHit> hitResults, Vector3 origin, Vector3 normal, float radius, int count, int layerMask)
    {
        hitResults = new List<RaycastHit>();

        if (GetFirstNonZeroIndex(out int nonZeroIndex, normal))
        {
            Vector3 planeVector1 = new();
            planeVector1[nonZeroIndex] = -normal[nonZeroIndex + 1 % 3] / normal[nonZeroIndex];
            planeVector1[nonZeroIndex + 1 % 3] = 1.0f;
            planeVector1[nonZeroIndex + 2 % 3] = 0.0f;
            Vector3.Normalize(planeVector1);

            Vector3 planeVector2 = new();
            planeVector2[nonZeroIndex] = -normal[nonZeroIndex + 2 % 3] / normal[nonZeroIndex];
            planeVector2[nonZeroIndex + 1 % 3] = 0.0f;
            planeVector2[nonZeroIndex + 2 % 3] = 1.0f;
            Vector3.Normalize(planeVector2);

            Vector3 rayOrigin = origin + planeVector1;
            float angleStep = 2 * Mathf.PI / count;
            float angle = angleStep;
            Vector3 nextPoint = origin + radius * (Mathf.Cos(angleStep) * planeVector1 + Mathf.Sin(angleStep) * planeVector2);

            for (int i = 0; i < count; ++i)
            {
                Vector3 rayDirection = nextPoint - rayOrigin;

                // Raycasts will not detect colliders for which the raycast origin is inside the collider. (cf unity doc)
                RaycastHit[] hits = Physics.RaycastAll(rayOrigin, rayDirection.normalized, rayDirection.magnitude, layerMask);

                hitResults.Capacity += hits.Length;
                for (int j = 0, resultIndex = hitResults.Count; j < hits.Length; ++j, ++resultIndex)
                {
                    hitResults[resultIndex] = hits[j];
                }

                rayOrigin = nextPoint;
                angle += angleStep;
                nextPoint = radius * (Mathf.Cos(angle) * planeVector1 + Mathf.Sin(angle) * planeVector2);
            }
        }

        return false;
    }

    public static bool GetFirstNonZeroIndex(out int index, Vector3 vector)
    {
        return GetFirstNonZeroIndex(out index, vector.x, vector.y, vector.z);
    }

    public static bool GetFirstNonZeroIndex(out int index, params float[] array)
    {
        index = 0;
        for (int i = 0; i < array.Length; ++i)
        {
            if (Mathf.Abs(array[index]) > Mathf.Epsilon)
            {
                index = i;
                return true;
            }
        }
        return false;
    }

    public static int GetMaxIndex(params float[] array)
    {
        int maxIndex = 0;
        float max = array[maxIndex];
        for (int i = 1; i < array.Length; ++i)
        {
            if (array[i] > max)
            {
                maxIndex = i;
                max = array[maxIndex];
            }
        }
        return maxIndex;
    }
}
