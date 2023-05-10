using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class River : MonoBehaviour
{
    [SerializeField] private float slowdown = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if(playerController != null)
            {
                playerController.SetPlayerSpeed(playerController.GetPlayerSpeed() * slowdown);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.SetPlayerSpeed(playerController.GetPlayerSpeed() / slowdown);
            }
        }
    }
}
