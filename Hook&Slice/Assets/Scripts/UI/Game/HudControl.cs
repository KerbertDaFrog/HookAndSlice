using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudControl : MonoBehaviour
{
    [Header("Collection")]
    [SerializeField]
    private GameObject collection;

    [Header("Player Health")]
    PlayerHealth ph;
    [SerializeField]
    private Slider hpSlider;
    [SerializeField]
    private Text hpText;

    [Header("Shoot Cooldown")]
    private Slider cooldownSlider;





    #region Hooking Things Up
    //hooking all the things up
    private void Awake()
    {
        ph = FindObjectOfType<PlayerHealth>();
    }

    //connect them:
    private void OnEnable()
    {
        ph.onHealthChange += hpChange;
    }

    //Dissconnect them:
    private void OnDisable()
    {
        ph.onHealthChange -= hpChange;
    }
    #endregion


    //Collection Of Items:

    public void CollectionON()
    {
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


    //HealthBar
    private void hpChange(float currentHealth, float maxHealth)
    {
        hpSlider.value = currentHealth / maxHealth;
        hpText.text = currentHealth + "/" + maxHealth;

    }

    private void cooldownHarpoon(float remaining)
    {
        cooldownSlider.value = remaining;
    }


}
