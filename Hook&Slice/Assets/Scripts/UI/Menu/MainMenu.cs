using System.Collections;
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
        Time.timeScale = 1;
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
        SceneManager.LoadScene(1);
    }

    public void SettingsON()
    {
        AudioManager.instance.Play("MenuButton");
        mainMenuPage.SetActive(false);
        settingsPage.SetActive(true);
        mainpage = false;
    }


    public void SettingsOFF()
    {
        AudioManager.instance.Play("MenuButton");
        mainMenuPage.SetActive(true);
        settingsPage.SetActive(false);
        mainpage = true;
    }

    public void CreditsON()
    {
        AudioManager.instance.Play("MenuButton");
        mainMenuPage.SetActive(false);
        creditsPage.SetActive(true);
        mainpage = false;
    }

    public void CreditsOFF()
    {
        AudioManager.instance.Play("MenuButton");
        mainMenuPage.SetActive(true);
        creditsPage.SetActive(false);
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
