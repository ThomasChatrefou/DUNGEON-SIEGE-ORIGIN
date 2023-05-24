using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceUIScript : MonoBehaviour
{
    [SerializeField] GameObject _upgradeChoiceHolder;
    [SerializeField] int _numberOfUpgrade = 3;
    [SerializeField] int _numberOfChoice = 2;
    [SerializeField] int _numberOfWeaponTradable = 2;
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

    float _halfScreenWidth;
    List<GameObject> _upgradeChoiceHolderList = new List<GameObject>();
    float _offset = 0;

    private void OnEnable()
    {
        _launchLevelTransitionChannel.OnEventTrigger += StartLevelTransition;
    }

    // Start is called before the first frame update
    private void Awake()
    {
        _halfScreenWidth = Screen.width / 2;
    }

    private void StartLevelTransition()
    {
        for (int i = 0; i < _numberOfChoice; i++)
        {
            _upgradeChoiceHolderList.Add(Instantiate(_upgradeChoiceHolder, transform, false));
            DisplayChoiceScript displayChoiceScript = _upgradeChoiceHolderList[i].GetComponent<DisplayChoiceScript>();
            if (i % 2 == 0)
            {

                _upgradeChoiceHolderList[i].transform.SetLocalPositionAndRotation(new Vector3((_halfScreenWidth / _numberOfChoice) * -1, 0), Quaternion.identity);
                displayChoiceScript.SetChoice(_tradeWeaponChoice);
                displayChoiceScript.SetChoiceType(EChoiceType.TRADESETUP);
            }
            else
            {

                _upgradeChoiceHolderList[i].transform.SetLocalPositionAndRotation(new Vector3((_halfScreenWidth / _numberOfChoice), 0), Quaternion.identity);
                displayChoiceScript.SetChoice(_upgradeWeaponChoice);
                displayChoiceScript.SetChoiceType(EChoiceType.UPGRADESETUP);
            }
        }
    }

    public void ChoseTrade()
    {
        foreach (GameObject gameObject in _upgradeChoiceHolderList)
        {
            Destroy(gameObject);
        }
        _upgradeChoiceHolderList.Clear();
        for (int i = 0; i < _numberOfWeaponTradable; i++)
        {
            _upgradeChoiceHolderList.Add(Instantiate(_upgradeChoiceHolder, transform, false));
            DisplayChoiceScript displayChoiceScript = _upgradeChoiceHolderList[i].GetComponent<DisplayChoiceScript>();
            if (i % 2 == 0)
            {
                _upgradeChoiceHolderList[i].transform.SetLocalPositionAndRotation(new Vector3((_halfScreenWidth / _numberOfChoice) * -1, 0), Quaternion.identity);
            }
            else
            {
                _upgradeChoiceHolderList[i].transform.SetLocalPositionAndRotation(new Vector3((_halfScreenWidth / _numberOfChoice), 0), Quaternion.identity);
            }
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
        foreach (GameObject gameObject in _upgradeChoiceHolderList)
        {
            Destroy(gameObject);
        }
        _upgradeChoiceHolderList.Clear();
        for (int i = 0; i < _numberOfUpgrade; i++)
        {
            _upgradeChoiceHolderList.Add(Instantiate(_upgradeChoiceHolder, transform, false));
            DisplayChoiceScript displayChoiceScript = _upgradeChoiceHolderList[i].GetComponent<DisplayChoiceScript>();
            if (i % 2 == 1 && i != 0)
            {
                _upgradeChoiceHolderList[i].transform.SetLocalPositionAndRotation(new Vector3(((_halfScreenWidth / _numberOfUpgrade) * -1 * 1.5f), 0), Quaternion.identity);
                displayChoiceScript.SetChoice(_upgradeOneChoice);
            }
            else if (i % 2 == 0 && i != 0)
            {
                _upgradeChoiceHolderList[i].transform.SetLocalPositionAndRotation(new Vector3(((_halfScreenWidth / _numberOfUpgrade) * 1.5f), 0), Quaternion.identity);
                displayChoiceScript.SetChoice(_upgradeThreeChoice);
            }
            else
            {
                _upgradeChoiceHolderList[i].transform.SetLocalPositionAndRotation(new Vector3(((_halfScreenWidth + _offset) / _numberOfUpgrade) * i, 0), Quaternion.identity);
                displayChoiceScript.SetChoice(_upgradeTwoChoice);
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
