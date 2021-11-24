﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameAnalyticsSDK;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuPage;
    private bool mainpage = true;

    [SerializeField]
    private GameObject settingsPage;

    [SerializeField]
    private GameObject creditsPage;

    [SerializeField]
    private GameObject quitPromptPage;

    [SerializeField]
    private GameObject liveMage;

    [SerializeField]
    private GameObject corpses;

    [SerializeField]
    private bool win;

    //[SerializeField]
    //private Animator cameraAnim;

    // Start is called before the first frame update
    void Start()
    {
        GameAnalytics.Initialize();
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Main Menu");
        mainMenuPage.SetActive(true);
        settingsPage.SetActive(false);
        creditsPage.SetActive(false);
        quitPromptPage.SetActive(false);
        mainpage = true;
        Time.timeScale = 1;
        win = SettingsManager.Instance.winstate;
        if(win)
        {
            corpses.SetActive(true);
            liveMage.SetActive(false);
        }
        else
        {
            corpses.SetActive(false);
            liveMage.SetActive(true);
        }
        //cameraAnim.SetBool("settings", false);
        //cameraAnim.SetBool("credits", false);
    }



    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && mainpage == false)
        {
            mainMenuPage.SetActive(true);
            settingsPage.SetActive(false);
            creditsPage.SetActive(false);
            quitPromptPage.SetActive(false);
            AudioManager.instance.Play("MenuButton");
            mainpage = true;
        }
    }


    public void StartGame()
    {
        AudioManager.instance.Play("MenuButton");
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Game");
        SettingsManager.Instance.winstate = false;
        SceneManager.LoadScene(1);
    }

    public void SettingsON()
    {
        AudioManager.instance.Play("MenuButton");
        mainMenuPage.SetActive(false);
        settingsPage.SetActive(true);
        //cameraAnim.SetBool("settings", true);
        mainpage = false;
    }


    public void SettingsOFF()
    {
        AudioManager.instance.Play("MenuButton");
        mainMenuPage.SetActive(true);
        settingsPage.SetActive(false);
        //cameraAnim.SetBool("settings", false);
        mainpage = true;
    }

    public void CreditsON()
    {
        AudioManager.instance.Play("MenuButton");
        mainMenuPage.SetActive(false);
        creditsPage.SetActive(true);
        //cameraAnim.SetBool("credits", false);
        mainpage = false;
    }

    public void CreditsOFF()
    {
        AudioManager.instance.Play("MenuButton");
        mainMenuPage.SetActive(true);
        creditsPage.SetActive(false);
        //cameraAnim.SetBool("credits", false);
        mainpage = true;
    }

    public void QuitpromptON()
    {
        AudioManager.instance.Play("MenuButton");
        mainMenuPage.SetActive(false);
        quitPromptPage.SetActive(true);
        mainpage = false;
    }


    public void QuitpromptOFF()
    {
        AudioManager.instance.Play("MenuButton");
        mainMenuPage.SetActive(true);
        quitPromptPage.SetActive(false);
        mainpage = true;
    }

    public void QuitGame()
    {
        AudioManager.instance.Play("MenuButton");
        Application.Quit();
    }

}
