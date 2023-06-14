using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [SerializeField] public GameObject settingsPanel;
    [SerializeField] public GameObject menuPanel;
    [SerializeField] public GameObject weaponsPanel;
    SceneTransition transit;

    void Start()
    {
        transit = gameObject.GetComponent<SceneTransition>();
        HideSettings();
        HideWeapons();
    }

    /* 
    Functions for the buttons, each button hides the previous screen and shows the new screen
    */
    public void MenuPlay()
    {
        transit.LoadPlayScene();
    }

    public void MenuSettings()
    {
        HideMenu();
        ShowSettings();
    }

    public void MenuWeapons()
    {
        HideMenu();
        ShowWeapons();
    }

    public void WeaponsBack()
    {
        HideWeapons();
        ShowMenu();
    }


    public void SettingsReset()
    {
        /* 
        Reset the datas of the player
        */
    }

    public void SettingsBack()
    {
        HideSettings();
        ShowMenu();
    }

    public void SettingsQuit()
    {
        Application.Quit();
    }


    /*
    Functions to call when needed, each one is called by its button function above
    */

    public void ShowSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void HideSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void ShowMenu()
    {
        menuPanel.SetActive(true);
    }

    public void HideMenu()
    {
        menuPanel.SetActive(false);
    }

    public void ShowWeapons()
    {
        weaponsPanel.SetActive(true);
    }

    public void HideWeapons()
    {
        weaponsPanel.SetActive(false);
    }
}
