using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum EChoiceType
{
    NONE,
    TRADE,
    UPGRADE,
    TRADESETUP,
    UPGRADESETUP
}

public class DisplayChoiceScript : MonoBehaviour
{
    public string _sceneToLoad;

    [SerializeField] TextMeshProUGUI _topText;
    [SerializeField] TextMeshProUGUI _bottomText;

    Choice _choice;
    EChoiceType _type = EChoiceType.NONE;
    List<GameObject> _otherChoicesInCurrentChoice = new List<GameObject>();
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
            case EChoiceType.UPGRADESETUP:
                Debug.Log("UpgradeSetup");
                transform.GetComponentInParent<ChoiceUIScript>().ChoseUpgrade();
                break;
            default:
                break;
        }
        //foreach (GameObject choice in _otherChoicesInCurrentChoice)
        //{
        //    Destroy(choice);
        //}
    }

    public void SetChoice(Choice choice)
    {
        _choice = choice;
        _topText.text = _choice.name;
        _bottomText.text = _choice.description;
    }

    public void SetChoiceType(EChoiceType choiceType)
    {
        _type = choiceType;
    }

    public void AddChoice(GameObject newChoice)
    {
        _otherChoicesInCurrentChoice.Add(newChoice);
    }
}
