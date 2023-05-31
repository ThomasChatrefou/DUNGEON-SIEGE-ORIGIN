using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{

    [SerializeField] private VoidEventChannelSO _playerDiedEventChannel;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject buttonPause;

    /* Setting the event for the player's death */
    private void OnEnable()
    {
        _playerDiedEventChannel.OnEventTrigger += ShowDeathScreen;
        HideDeathScreen();
    }

    private void OnDisable()
    {
        _playerDiedEventChannel.OnEventTrigger -= ShowDeathScreen;
    }

    /* Manage Death Screen in the UI*/
    private void ShowDeathScreen()
    {
        deathScreen.SetActive(true);
        Debug.Log("Show death screen");
    }

    public void HideDeathScreen()
    {
        deathScreen.SetActive(false);
        Debug.Log("Hide death screen");
    }


    /* Behavior when clicking pause */
    public void Pause()
    {
        
    }

}
