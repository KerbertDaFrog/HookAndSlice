﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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



    // Start is called before the first frame update
    void Start()
    {
        //Add Game Analytics initilisation here.
        mainMenuPage.SetActive(true);
        settingsPage.SetActive(false);
        creditsPage.SetActive(false);
        quitPromptPage.SetActive(false);
        mainpage = true;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && mainpage == false)
        {
            mainMenuPage.SetActive(true);
            settingsPage.SetActive(false);
            creditsPage.SetActive(false);
            quitPromptPage.SetActive(false);
            mainpage = true;
        }
    }


    public void StartGame()
    {
        //UI audio here
        SceneManager.LoadScene(1);
    }

    public void SettingsON()
    {
        //UI audio here
        mainMenuPage.SetActive(false);
        settingsPage.SetActive(true);
        mainpage = false;
    }


    public void SettingsOFF()
    {
        //UI audio here
        mainMenuPage.SetActive(true);
        settingsPage.SetActive(false);
        mainpage = true;
    }

    public void CreditsON()
    {
        //UI audio here
        mainMenuPage.SetActive(false);
        creditsPage.SetActive(true);
        mainpage = false;
    }

    public void CreditsOFF()
    {
        //UI audio here
        mainMenuPage.SetActive(true);
        creditsPage.SetActive(false);
        mainpage = true;
    }

    public void QuitpromptON()
    {
        //UI audio here
        mainMenuPage.SetActive(false);
        quitPromptPage.SetActive(true);
        mainpage = false;
    }


    public void QuitpromptOFF()
    {
        //UI audio here
        mainMenuPage.SetActive(true);
        quitPromptPage.SetActive(false);
        mainpage = true;
    }

    public void QuitGame()
    {
        //UI audio here
        Application.Quit();
    }

}
