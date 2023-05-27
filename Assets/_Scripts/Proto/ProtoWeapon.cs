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
    [SerializeField] private int damageZoneRefinement = 10;
    [SerializeField] private int damages = 1;
    [SerializeField] private GameObject damageZonePrefab;

    [Range(0f, 360f)]
    [SerializeField] private float normalDegLatitude;
    [Range(0f, 180f)]
    [SerializeField] private float normalDegLongitude;
    [SerializeField] private float normalDisplayMagnitude = 2f;

    public void Use()
    {

        GameObject damageZoneGO = Instantiate(damageZonePrefab, transform.position, damageZonePrefab.transform.rotation);
        damageZoneGO.transform.localScale *= damageZoneRadius;
        Destroy(damageZoneGO, damageZoneLifeTime);
        
        int bitShiftedLayerMask = 1 << LayerMask.NameToLayer(hitableLayerName);

        if (CircleCaster.CircleCast(out List<Collider> hitColliders, transform.position, Vector3.up, damageZoneRadius, damageZoneRefinement, bitShiftedLayerMask))
        {
            foreach (Collider collider in hitColliders)
            {
                ICharacterHealth healthComponent = collider.gameObject.GetComponent<ICharacterHealth>();
                healthComponent?.TakeDamage(damages);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 origin = transform.position;

        float radLatitude = Mathf.Deg2Rad * normalDegLatitude;
        float radLongitude = Mathf.Deg2Rad * normalDegLongitude;
        Vector3 normal = new(Mathf.Sin(radLongitude) * Mathf.Cos(radLatitude), 
                             Mathf.Cos(radLongitude), 
                             Mathf.Sin(radLongitude) * Mathf.Sin(radLatitude));

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, normal * normalDisplayMagnitude);

        //Vector3 normal = Vector3.up + Vector3.right;
        float radius = damageZoneRadius;
        int count = damageZoneRefinement;

        if (CircleCaster.GetFirstNonZeroIndex(out int nonZeroIndex, normal))
        {
            Vector3 planeVector1 = new();
            planeVector1[nonZeroIndex] = -normal[(nonZeroIndex + 1) % 3] / normal[nonZeroIndex];
            planeVector1[(nonZeroIndex + 1) % 3] = 1.0f;
            planeVector1[(nonZeroIndex + 2) % 3] = 0.0f;
            planeVector1 = Vector3.Normalize(planeVector1);

            Vector3 planeVector2 = new();
            planeVector2[nonZeroIndex] = -normal[(nonZeroIndex + 2) % 3] / normal[nonZeroIndex];
            planeVector2[(nonZeroIndex + 1) % 3] = 0.0f;
            planeVector2[(nonZeroIndex + 2) % 3] = 1.0f;

            planeVector2 = Vector3.Cross(planeVector1, normal);
            planeVector2 = Vector3.Normalize(planeVector2);

            Vector3 rayOrigin = origin + radius * planeVector1;
            float angleStep = 2 * Mathf.PI / count;
            float angle = angleStep;
            Vector3 nextPoint = origin + radius * (Mathf.Cos(angleStep) * planeVector1 + Mathf.Sin(angleStep) * planeVector2);

            for (int i = 0; i < count; ++i)
            {
                Vector3 rayDirection = nextPoint - rayOrigin;

                Gizmos.color = Color.red;
                Gizmos.DrawRay(rayOrigin, rayDirection);

                rayOrigin = nextPoint;
                angle += angleStep;
                nextPoint = origin + radius * (Mathf.Cos(angle) * planeVector1 + Mathf.Sin(angle) * planeVector2);
            }
        }
    }
}

public class CircleCaster
{
    public static bool CircleCast(out List<Collider> hitColliders, Vector3 origin, Vector3 normal, float radius, int count, int layerMask)
    {
        hitColliders = new List<Collider>();

        if (GetFirstNonZeroIndex(out int nonZeroIndex, normal))
        {
            Vector3 planeVector1 = new();
            planeVector1[nonZeroIndex] = -normal[(nonZeroIndex + 1) % 3] / normal[nonZeroIndex];
            planeVector1[(nonZeroIndex + 1) % 3] = 1.0f;
            planeVector1[(nonZeroIndex + 2) % 3] = 0.0f;
            planeVector1 = Vector3.Normalize(planeVector1);

            Vector3 planeVector2 = new();
            planeVector2[nonZeroIndex] = -normal[(nonZeroIndex + 2) % 3] / normal[nonZeroIndex];
            planeVector2[(nonZeroIndex + 1) % 3] = 0.0f;
            planeVector2[(nonZeroIndex + 2) % 3] = 1.0f;
            planeVector2 = Vector3.Normalize(planeVector2);

            Vector3 rayOrigin = origin + radius * planeVector1;
            float angleStep = 2 * Mathf.PI / count;
            float angle = angleStep;
            Vector3 nextPoint = origin + radius * (Mathf.Cos(angleStep) * planeVector1 + Mathf.Sin(angleStep) * planeVector2);

            for (int i = 0; i < count; ++i)
            {
                Vector3 rayDirection = nextPoint - rayOrigin;

                // Raycasts will not detect colliders for which the raycast origin is inside the collider. (cf unity doc)
                RaycastHit[] hits = Physics.RaycastAll(rayOrigin, rayDirection.normalized, rayDirection.magnitude, layerMask);

                hitColliders.Capacity += hits.Length;
                for (int j = 0, resultIndex = hitColliders.Count; j < hits.Length; ++j, ++resultIndex)
                {
                    hitColliders.Add(hits[j].collider);
                }

                rayOrigin = nextPoint;
                angle += angleStep;
                nextPoint = origin + radius * (Mathf.Cos(angle) * planeVector1 + Mathf.Sin(angle) * planeVector2);
            }

            if (hitColliders.Count > 0)
            {
                return true;
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
            if (Mathf.Abs(array[i]) > Mathf.Epsilon)
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
