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
    [SerializeField] float _fadeTimer;
    [SerializeField] GameObject _backgroundGO;
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
        _backgroundGO.SetActive(true);
        StartCoroutine(BackgroundFadeIn());
    }

    private void AfterFade()
    {
        _twoChoice.SetActive(true);
        StartCoroutine(TwoChoiceFadeIn());
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
        StartCoroutine(TwoChoiceFadeIn());
        StartCoroutine(TwoChoiceFadeOut());
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
        StartCoroutine(TwoChoiceFadeOut());
        _twoChoice.SetActive(false);
        _threeChoice.SetActive(true);
        StartCoroutine(ThreeChoiceFadeIn());

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


    private IEnumerator BackgroundFadeIn()
    {
        Image _backgroundImage = _backgroundGO.GetComponent<Image>();
        for (float i = 0; i <= _fadeTimer; i += Time.fixedDeltaTime)
        {
            _backgroundImage.color = new Color(0, 0, 0, i);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }        
        yield return new WaitForEndOfFrame();
        AfterFade();
    }

    private IEnumerator TwoChoiceFadeIn()
    {
        Image ChoiceOneImage = _twoChoiceList[0].GetComponentInChildren<Image>();
        Image ChoiceTwoImage = _twoChoiceList[1].GetComponentInChildren<Image>();

        for (float i = 0; i <= _fadeTimer / 2; i += Time.fixedDeltaTime)
        {
            ChoiceOneImage.color = new Color(1, 1, 1, i);
            ChoiceTwoImage.color = new Color(1, 1, 1, i);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        yield return new WaitForEndOfFrame();
    }

    private IEnumerator TwoChoiceFadeOut()
    {
        Image ChoiceOneImage = _twoChoiceList[0].GetComponentInChildren<Image>();
        Image ChoiceTwoImage = _twoChoiceList[1].GetComponentInChildren<Image>();

        for (float i = 0; i >= _fadeTimer / 2; i -= Time.fixedDeltaTime)
        {
            ChoiceOneImage.color = new Color(1, 1, 1, i);
            ChoiceTwoImage.color = new Color(1, 1, 1, i);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        yield return new WaitForEndOfFrame();
    }

    private IEnumerator ThreeChoiceFadeIn()
    {
        Image ChoiceOneImage = _threeChoiceList[0].GetComponentInChildren<Image>();
        Image ChoiceTwoImage = _threeChoiceList[1].GetComponentInChildren<Image>();
        Image ChoiceThreeImage = _threeChoiceList[2].GetComponentInChildren<Image>();

        for (float i = 0; i <= _fadeTimer / 2; i += Time.fixedDeltaTime)
        {
            ChoiceOneImage.color = new Color(1, 1, 1, i);
            ChoiceTwoImage.color = new Color(1, 1, 1, i);
            ChoiceThreeImage.color = new Color(1, 1, 1, i);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        yield return new WaitForEndOfFrame();
    }

    private IEnumerator ThreeChoiceFadeOut()
    {
        Image ChoiceOneImage = _threeChoiceList[0].GetComponentInChildren<Image>();
        Image ChoiceTwoImage = _threeChoiceList[1].GetComponentInChildren<Image>();
        Image ChoiceThreeImage = _threeChoiceList[2].GetComponentInChildren<Image>();

        for (float i = 0; i >= _fadeTimer / 2; i -= Time.fixedDeltaTime)
        {
            ChoiceOneImage.color = new Color(1, 1, 1, i);
            ChoiceTwoImage.color = new Color(1, 1, 1, i);
            ChoiceThreeImage.color = new Color(1, 1, 1, i);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        yield return new WaitForEndOfFrame();
    }
}
