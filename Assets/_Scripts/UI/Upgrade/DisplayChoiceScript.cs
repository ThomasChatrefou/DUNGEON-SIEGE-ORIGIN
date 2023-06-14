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

public class DisplayChoiceScript : MonoBehaviour
{
    [SerializeField] Image _backgroundImage;

    public string _sceneToLoad;
    Choice _choice;
    EChoiceType _type = EChoiceType.NONE;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        switch (_type)
        {
            case EChoiceType.NONE:
                Debug.Log("Something went wrong ...");
                break;
            case EChoiceType.TRADE:
                Debug.Log("Trade -> TODO: Actual trade weapon logic");
                SceneManager.LoadSceneAsync(_sceneToLoad);
                break;
            case EChoiceType.UPGRADE:
                Debug.Log("Upgrade -> TODO: Actual upgrade weapon logic");
                SceneManager.LoadSceneAsync(_sceneToLoad);
                break;
            case EChoiceType.TRADESETUP:
                Debug.Log("TradeSetup");
                transform.GetComponentInParent<ChoiceUIScript>().ChoseTrade();
                break;
            default:
                break;
        }
        //foreach (GameObject choice in _otherChoicesInCurrentChoice)
        //{
        //    Destroy(choice);
        //}
    }
    public void SetChoiceType(EChoiceType choiceType)
    {
        _type = choiceType;
    }

    public void SetImageSprite(Sprite newSprite)
    {
        _backgroundImage.sprite = newSprite;
    }
}
