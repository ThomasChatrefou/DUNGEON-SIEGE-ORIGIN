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
            dealDamage = StartCoroutine(DealDamagePerSecond());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            StopCoroutine(dealDamage);
        }
    }

    IEnumerator DealDamagePerSecond()
    {
        while (true)
        {
            // Call for the function to take damage
            Debug.Log("deal damage");
            yield return new WaitForSeconds(timeBetweenTwoTick);
        }
    }
}
