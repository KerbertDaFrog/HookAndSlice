using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    public static GM instance;

    public GameState state;

    public GameObject deathScreen;

    public GameObject winScreen;

    void Awake()
    {
        instance = this;
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch(newState)
        {
            case GameState.LevelComplete:
                LevelComplete();
                break;
            case GameState.GameOver:
                GameOver();
                break;
        }
    }

    public void GameOver()
    {
        deathScreen.SetActive(true); 
    }

    public void LevelComplete()
    {
        winScreen.SetActive(true);
    }

    public enum GameState
    {
        LevelComplete,
        GameOver,
    }
}
