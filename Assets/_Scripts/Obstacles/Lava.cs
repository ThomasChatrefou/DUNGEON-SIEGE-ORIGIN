using System.Collections;
using UnityEngine;

public class Lava: MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float timeBetweenTwoTick = 1f;

    private Coroutine dealDamage = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            ICharacterHealth healthComponant = other.gameObject.GetComponent<ICharacterHealth>();
            if (healthComponant != null)
            {
                dealDamage = StartCoroutine(DealDamagePerSecond(healthComponant));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            StopCoroutine(dealDamage);
        }
    }

    IEnumerator DealDamagePerSecond(ICharacterHealth _healthComponant)
    {
        while (true)
        {
            _healthComponant.TakeDamage(damage);
            // Call for the function to take damage
            Debug.Log("deal damage");
            yield return new WaitForSeconds(timeBetweenTwoTick);
        }
    }
}
