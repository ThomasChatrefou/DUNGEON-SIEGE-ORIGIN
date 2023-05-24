using NaughtyAttributes;
using UnityEngine;

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
