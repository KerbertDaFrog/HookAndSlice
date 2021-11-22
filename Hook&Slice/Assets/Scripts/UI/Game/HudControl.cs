﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudControl : MonoBehaviour
{
    private static HudControl _instance;

    [Header("Collection")]

    public Pickup[] pickups;
    [SerializeField]
    private GameObject collection;
    [SerializeField]
    private Text collectionText;


    [Header("Player Health")]
    
    public PlayerHealth ph;
    [SerializeField]
    private Slider hpSlider;
    [SerializeField]
    private Text hpText;


    [Header("Shoot Cooldown")]
    public Harpoon harpoon;
    [SerializeField]
    private Slider cooldownSlider;


    [Header("Door Interactions")]

    public DoorOpen[] doors;
    [SerializeField]
    private GameObject icon;
    [SerializeField]
    private GameObject lockedMessage;

    [Header("GameScreens")]

    
    [SerializeField]
    private GameObject deathScreen;
    [SerializeField]
    private GameObject winScreen;


    [Header("Pause Menu")]
    public PauseMenu pm;


    [Header("Kill Count")]
    [SerializeField]
    private Text killCountText;
    public int killCount;

    [Header("Stats")]
    [SerializeField]
    private Text timeSpentText;
    [SerializeField]
    private Text enmiesKilledText;
    [SerializeField]
    private Text shotHooksText;
    [SerializeField]
    private Text secretsText;
    //[SerializeField]
    //private Text deathsText;

    private int hooksShot;
    private int secretsFound;
    //private int deaths;

    public static HudControl Instance { get { return _instance; } }

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

        pm = FindObjectOfType<PauseMenu>();

        pickups = FindObjectsOfType<Pickup>();
        ph = FindObjectOfType<PlayerHealth>();
        harpoon = FindObjectOfType<Harpoon>();
        doors = FindObjectsOfType<DoorOpen>();
        cooldownSlider.maxValue = 1f;
    }


    //connect them:
    private void OnEnable()
    {
        foreach(Pickup p in pickups)
            p.confirmHUD += CollectionON;
        
        ph.onHealthChange += hpChange;
        ph.death += DeathScreen;
        harpoon.harpoonCooldown += cooldownHarpoon;
        foreach (DoorOpen d in doors)
            d.interaction += DoorInteraction;
        foreach (DoorOpen o in doors)
            o.doorLocked += DoorLockedMessage;

    }

    //Dissconnect them:
    private void OnDisable()
    {
        foreach(Pickup p in pickups)
            p.confirmHUD -= CollectionON;
        ph.onHealthChange -= hpChange;
        ph.death -= DeathScreen;
        harpoon.harpoonCooldown -= cooldownHarpoon;
        foreach(DoorOpen o in doors)
            o.interaction -= DoorInteraction;
    }



    //Collection Of Items:
    #region Item Collection Feedback
    private void CollectionON(string pickupType)
    {        
        collectionText.text = "Collected: " + pickupType;
        collection.SetActive(true);
        StartCoroutine(FeedbackOFF());
    }

    IEnumerator FeedbackOFF()
    {
        yield return new WaitForSeconds(1.5f);
        CollectionOFF();
    }

    //Collection Confirmation turn off?
    public void CollectionOFF()
    {
        collection.SetActive(false);
    }
    #endregion

    //Key Display Icons:

    private void KeyIcon()
    {

    }

    //Enemy Kill Count Tally

    public void EnemyKillCount()
    {
        killCount++;
        killCountText.text = "Kill Count: " + killCount;
    }


    //HealthBar
    private void hpChange(float currentHealth, float maxHealth)
    {
        hpSlider.value = currentHealth;
        hpText.text = currentHealth + "/" + maxHealth;

    }

    private void cooldownHarpoon(float remaining, float max)
    {
        cooldownSlider.value = remaining;
    }


    //Door Interactions:

    private void DoorInteraction(bool interact)
    {
        icon.SetActive(interact);
    }

    private void DoorLockedMessage(bool lockeddoor)
    {
        lockedMessage.SetActive(lockeddoor);
    }


    private void DeathScreen()
    {
        deathScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        AudioManager.instance.StopPlaying("ChainMovement");
        AudioManager.instance.StopPlaying("DungeonMusic");
        AudioManager.instance.StopPlaying("KnightMusic");
        AudioManager.instance.Play("GameOver");
        pm.ExternalPause();
    }

    public void WinScreen()
    {
        Debug.Log("win");
        winScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        SetStats();
        AudioManager.instance.StopPlaying("ChainMovement");
        AudioManager.instance.StopPlaying("KnightMusic");
        pm.ExternalPause();
    }
    
    private void SetStats()
    {
        enmiesKilledText.text = killCount.ToString();
        shotHooksText.text = hooksShot.ToString();
        secretsText.text = secretsFound.ToString();
    }


    private void ClearStats()
    {
        killCount = 0;
        hooksShot = 0;
        secretsFound = 0;
        //deaths = 0;
    }


    private void OnApplicationQuit()
    {
        _instance = null;
    }

}
