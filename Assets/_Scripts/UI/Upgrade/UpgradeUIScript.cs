using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUIScript : MonoBehaviour
{
    [SerializeField] GameObject _upgradeChoiceHolder;
    [SerializeField] int _numberOfUpgrade = 3;
    [SerializeField] int _numberOfChoice = 2;
    [SerializeField] int _numberOfWeaponTradable = 2;

    [Header("Scriptable Object")]
    [SerializeField] Choice _upgradeWeaponChoice;
    [SerializeField] Choice _tradeWeaponChoice;

    float _halfScreenWidth;
    List<GameObject> _upgradeChoiceHolderList = new List<GameObject>();
    float _offset = 0;

    // Start is called before the first frame update
    private void Awake()
    {
        _halfScreenWidth = Screen.width / 2;
    }

    void Start()
    {
        for (int i = 0; i < _numberOfChoice; i++)
        {
            _upgradeChoiceHolderList.Add(Instantiate(_upgradeChoiceHolder, transform, false));
            if (i % 2 == 0)
            {
                _upgradeChoiceHolderList[i].transform.SetLocalPositionAndRotation(new Vector3((_halfScreenWidth / _numberOfChoice) * -1, 0), Quaternion.identity);
                _upgradeChoiceHolderList[i].GetComponent<UpgradeChoiceScript>().SetChoice(_tradeWeaponChoice);
                _upgradeChoiceHolderList[i].GetComponent<UpgradeChoiceScript>().SetIsATrade(true);
            }
            else
            {
                _upgradeChoiceHolderList[i].transform.SetLocalPositionAndRotation(new Vector3((_halfScreenWidth / _numberOfChoice), 0), Quaternion.identity);
                _upgradeChoiceHolderList[i].GetComponent<UpgradeChoiceScript>().SetChoice(_upgradeWeaponChoice);
                _upgradeChoiceHolderList[i].GetComponent<UpgradeChoiceScript>().SetIsATrade(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
            if (i % 2 == 0)
            {
                _upgradeChoiceHolderList[i].transform.SetLocalPositionAndRotation(new Vector3((_halfScreenWidth / _numberOfChoice) * -1, 0), Quaternion.identity);
            }
            else
            {
                _upgradeChoiceHolderList[i].transform.SetLocalPositionAndRotation(new Vector3((_halfScreenWidth / _numberOfChoice), 0), Quaternion.identity);
            }
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
            if (i % 2 == 1 && i != 0)
            {
                _upgradeChoiceHolderList[i].transform.SetLocalPositionAndRotation(new Vector3(((_halfScreenWidth / _numberOfUpgrade) * -1 * 1.5f), 0), Quaternion.identity);
            }
            else if (i % 2 == 0 && i != 0)
            {
                _upgradeChoiceHolderList[i].transform.SetLocalPositionAndRotation(new Vector3(((_halfScreenWidth / _numberOfUpgrade) * 1.5f), 0), Quaternion.identity);
            }
            else
            {
                _upgradeChoiceHolderList[i].transform.SetLocalPositionAndRotation(new Vector3(((_halfScreenWidth + _offset) / _numberOfUpgrade) * i, 0), Quaternion.identity);
            }
        }
    }

}
