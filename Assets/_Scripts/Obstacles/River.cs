using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class River : MonoBehaviour
{
    [SerializeField] private float _slowdown = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        RiverInteraction riverInteractionComponent = other.GetComponent<RiverInteraction>();
        if (riverInteractionComponent != null)
        {
            riverInteractionComponent.SlowCharacterSpeed(_slowdown);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        RiverInteraction riverInteractionComponent = other.GetComponent<RiverInteraction>();
        if (riverInteractionComponent != null)
        {
            riverInteractionComponent.RemoveSlowCharacterSpeed(_slowdown);
        }
    }
}
