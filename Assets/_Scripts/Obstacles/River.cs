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
            if(other.gameObject.CompareTag("Player"))
                riverInteractionComponent.SlowPlayerSpeed(_slowdown);
            else if(other.gameObject.CompareTag("AI"))
            Debug.Log(other.gameObject.name);
                riverInteractionComponent.SlowAiSpeed(_slowdown);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        RiverInteraction riverInteractionComponent = other.GetComponent<RiverInteraction>();
        if (riverInteractionComponent != null)
        {
            if (other.gameObject.CompareTag("Player"))
                riverInteractionComponent.RemoveSlowPlayerSpeed(_slowdown);
            else if (other.gameObject.CompareTag("AI"))
                riverInteractionComponent.RemoveSlowAiSpeed(_slowdown);
        }
    }
}
