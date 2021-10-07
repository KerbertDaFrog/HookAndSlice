using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int setHealth;
    [SerializeField]
    private int currentHealth;
    [SerializeField]
    private int damageTaken;
    [SerializeField]
    private int healthRestored;

    [SerializeField]
    private Slider hpBar;

    [SerializeField]
    private bool dead;
    [SerializeField]
    private PlayerLook pl;

    [SerializeField]
    private GameObject deathScreen;

    [SerializeField]
    private Hook hook;

    // Start is called before the first frame update
    private void Start()
    {
        currentHealth = setHealth;
        hpBar.maxValue = setHealth;
        hpBar.value = currentHealth;
    }

    // Update is called once per frame
    private void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, setHealth);
        hpBar.value = currentHealth;

        if (currentHealth <= 0 && !dead)
        {
            dead = true;
            KillPlayer();
        }       
    }

    private void TakeDamage()
    {
        Debug.Log("oof");
        currentHealth -= damageTaken;
        hpBar.value = currentHealth;
    }

    private void KillPlayer()
    {
        if(dead)
        {
            Time.timeScale = 0;
            deathScreen.SetActive(true);
            pl.Paused();
            Debug.Log("i ded");
            //Restart scene or something
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "DamageBox")
        {
            TakeDamage();
        }

        if (other.gameObject.tag == "HealthPotion")
        {
            RestoreHealth();
        }
            
    }

    private void RestoreHealth()
    {
        Debug.Log("Healing-yay");
        currentHealth += healthRestored;
        hpBar.value = currentHealth;
    }

}
