﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private PlayerLook pl;

    //[SerializeField]
    //private Hook hook;

    //[SerializeField]
    //private GameObject settingsPage;
    
    //[SerializeField]
    //private GameObject leavePopUp;

    //[SerializeField]
    //private GameObject quitPopUp;

    public bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                UnpauseGame();
            }
            else if(!paused)
            {
                PauseGame();
            }

        }
    }

    //pausing the game
    private void PauseGame()
    {
        Time.timeScale = 0;
        paused = true;
        
        pauseMenu.SetActive(true);
        pl.Paused();
    }

    //unpausing the game
    public void UnpauseGame()
    {
        Time.timeScale = 1;
        paused = false;
        pl.UnPaused();
        pauseMenu.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
