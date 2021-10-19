using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelComplete : MonoBehaviour
{
    [SerializeField]
    private GameObject winLevelScreen;

    //the things being displayed
    private int timeInlevelInt;
    private int enemiesKilledInt;
    private int hooksShotInt;
    private int secretsFoundInt;

    //The Text Objects for the stats
    //[SerializeField] private Text timeText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            WinLevel();
        }
    }


    private void WinLevel()
    {
        Time.timeScale = 0;
        winLevelScreen.SetActive(true);
        //timeText.text = "" + timeInlevelInt;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        AudioManager.instance.StopPlaying("ChainMovement");

    }

}
