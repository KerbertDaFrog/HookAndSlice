using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu _instance;

    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private PlayerLook pl;

    //[SerializeField]
    //private Hook hook;

    [SerializeField]
    private GameObject settingsPage;

    [SerializeField]
    private GameObject pauseButtons;

    //[SerializeField]
    //private GameObject leavePopUp;

    //[SerializeField]
    //private GameObject quitPopUp;

    public bool paused = false;

    public delegate void IsGamePaused(bool paused);
    public IsGamePaused isGamePaused;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

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
        pauseButtons.SetActive(true);
        settingsPage.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isGamePaused(paused);
        AudioManager.instance.StopPlaying("ChainMovement");
    }

    //unpausing the game
    public void UnpauseGame()
    {
        Time.timeScale = 1;
        paused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isGamePaused(paused);
        pauseMenu.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SettingsON()
    {
        settingsPage.SetActive(true);
        pauseButtons.SetActive(false);

    }

    public void SettingsOFF()
    {
        settingsPage.SetActive(false);
        pauseButtons.SetActive(true);
    }


    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
