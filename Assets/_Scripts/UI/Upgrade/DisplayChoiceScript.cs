using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum EChoiceType
{
    NONE,
    TRADE,
    UPGRADE,
    TRADESETUP,
}

public enum ETradeType
{
    NONE,
    SWORD,
    BOOK,
    WAND,
}

public class DisplayChoiceScript : MonoBehaviour, IDataPersistence
{
    [SerializeField] Image _backgroundImage;

    public string _sceneToLoad;
    Choice _choice;
    EChoiceType _choiceType = EChoiceType.NONE;
    ETradeType _tradeType = ETradeType.NONE;

    byte newWeaponID = 255;
    bool upgradeWeapon = false;

    public void OnClick()
    {
        switch (_choiceType)
        {
            case EChoiceType.NONE:
                Debug.Log("Something went wrong ...");
                break;
            case EChoiceType.TRADE:
                Trade();
                break;
            case EChoiceType.UPGRADE:
                Upgrade();
                break;
            case EChoiceType.TRADESETUP:
                Debug.Log("TradeSetup");
                transform.GetComponentInParent<ChoiceUIScript>().ChoseTrade();
                break;
            default:
                break;
        }
    }

    public void Upgrade()
    {
        upgradeWeapon = true;
        DataPersistenceManager.instance.GetPlayerDataSO().IncrementUpgrade();
        LoadNextScene();
    }

    public void Trade()
    {
        switch (_tradeType)
        {
            case ETradeType.NONE:
                Debug.Log("Something went wrong ...");
                break;
            case ETradeType.SWORD:
                newWeaponID = 0;
                break;
            case ETradeType.BOOK:
                newWeaponID = 1;
                break;
            case ETradeType.WAND:
                newWeaponID = 2;
                break;
            default:
                break;
        }
        LoadNextScene();
    }

    public void LoadNextScene()
    {
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync(_sceneToLoad);
    }


    public void SetChoiceType(EChoiceType choiceType)
    {
        _choiceType = choiceType;
    }

    public void SetTradeType(ETradeType tradeType) 
    {
        _tradeType = tradeType;
    }

    public void SetImageSprite(Sprite newSprite)
    {
        _backgroundImage.sprite = newSprite;
    }

    public void LoadData(GameData data)
    {
        newWeaponID = data.weaponID;
    }

    public void SaveData(ref GameData data)
    {
        if (upgradeWeapon)
        {
            data.weaponUpgrade[data.weaponID] += 1;
        }
        else
        {
            data.weaponID = newWeaponID;
        }
    }
}
