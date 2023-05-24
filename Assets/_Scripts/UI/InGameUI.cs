using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{

    [SerializeField] private ProtoCharacterDiedEventChannelSO _playerDiedEventChannel;
    [SerializeField] private GameObject deathScreen;

    private void OnEnable()
    {
        _playerDiedEventChannel.OnCharacterDied += ShowDeathScreen;
        HideDeathScreen();
    }

    private void OnDisable()
    {
        _playerDiedEventChannel.OnCharacterDied -= ShowDeathScreen;
    }

    private void ShowDeathScreen()
    {
        deathScreen.SetActive(true);
    }

    public void HideDeathScreen()
    {
        deathScreen.SetActive(false);
    }

}
