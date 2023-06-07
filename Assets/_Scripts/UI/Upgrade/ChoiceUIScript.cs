using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceUIScript : MonoBehaviour
{
    [SerializeField] GameObject _upgradeChoiceHolder;
    [SerializeField] GameObject _twoChoice;
    [SerializeField] List<GameObject> _twoChoiceList = new List<GameObject>();
    [SerializeField] GameObject _threeChoice;
    [SerializeField] List<GameObject> _threeChoiceList = new List<GameObject>();
    [BoxGroup("Listen to")]
    [SerializeField] VoidEventChannelSO _launchLevelTransitionChannel;
    [Scene]
    [SerializeField] string _sceneToLoad;

    [Header("Scriptable Object")]
    [SerializeField] Choice _upgradeWeaponChoice;
    [SerializeField] Choice _tradeWeaponChoice;
    [SerializeField] Choice _swordWeaponChoice;
    [SerializeField] Choice _staffWeaponChoice;
    [SerializeField] Choice _bookWeaponChoice;
    [SerializeField] Choice _upgradeOneChoice;
    [SerializeField] Choice _upgradeTwoChoice;
    [SerializeField] Choice _upgradeThreeChoice;

    //Constants
    const int NumberOfFirstChoice = 2;
    const int NumberOfTradeChoice = 2;
    const int NumberOfUpgradeChoice = 3;

    private void OnEnable()
    {
        _launchLevelTransitionChannel.OnEventTrigger += StartLevelTransition;
    }

    private void StartLevelTransition()
    {

        _twoChoice.SetActive(true);
        for (int i = 0; i < NumberOfFirstChoice; i++)
        {
            DisplayChoiceScript displayChoiceScript = _twoChoiceList[i].GetComponent<DisplayChoiceScript>();
            switch (i)
            {
                case 0:
                    displayChoiceScript.SetChoice(_tradeWeaponChoice);
                    displayChoiceScript.SetChoiceType(EChoiceType.TRADESETUP);
                    break;
                case 1:
                    displayChoiceScript.SetChoice(_upgradeWeaponChoice);
                    displayChoiceScript.SetChoiceType(EChoiceType.UPGRADESETUP);
                    break;
                default:
                    break;
            }
        }
    }

    public void ChoseTrade()
    {
        for (int i = 0; i < NumberOfTradeChoice; i++)
        {
            DisplayChoiceScript displayChoiceScript = _twoChoiceList[i].GetComponent<DisplayChoiceScript>();
            int randomChoice = Random.Range(1, 3);
            switch (randomChoice)
            {
                case 1:
                    displayChoiceScript.SetChoice(_swordWeaponChoice);
                    break;
                case 2:
                    displayChoiceScript.SetChoice(_staffWeaponChoice);
                    break;
                case 3:
                    displayChoiceScript.SetChoice(_bookWeaponChoice);
                    break;
                default:
                    break;
            }
            displayChoiceScript.SetChoiceType(EChoiceType.TRADE);
            displayChoiceScript._sceneToLoad = _sceneToLoad;
        }
    }

    public void ChoseUpgrade()
    {
        _twoChoice.SetActive(false);
        _threeChoice.SetActive(true);
        for (int i = 0; i < NumberOfUpgradeChoice; i++)
        {
            DisplayChoiceScript displayChoiceScript = _threeChoiceList[i].GetComponent<DisplayChoiceScript>();
            switch (i)
            {
                case 0:
                    displayChoiceScript.SetChoice(_upgradeOneChoice);
                    break;
                case 1:
                    displayChoiceScript.SetChoice(_upgradeTwoChoice);
                    break;
                case 2:
                    displayChoiceScript.SetChoice(_upgradeThreeChoice);
                    break;
                default:
                    break;
            }
            displayChoiceScript.SetChoiceType(EChoiceType.UPGRADE);
            displayChoiceScript._sceneToLoad = _sceneToLoad;
        }
    }

    private void OnDisable()
    {
        _launchLevelTransitionChannel.OnEventTrigger -= StartLevelTransition;
    }

}
