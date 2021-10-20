using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudControl : MonoBehaviour
{
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

    #region Hooking Things Up
    //hooking all the things up
    private void Awake()
    {
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
        harpoon.harpoonCooldown -= cooldownHarpoon;
        foreach(DoorOpen o in doors)
            o.interaction -= DoorInteraction;
    }
    #endregion


    //Collection Of Items:
    #region
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

}
