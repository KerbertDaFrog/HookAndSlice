using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    private bool dead;
    [SerializeField]
    private bool triggered;

    [SerializeField]
    private Hook hook;

    [SerializeField]
    private PlayerLook pl;


    public delegate void OnHealthChange(float currentHealth, float maxHealth);
    public OnHealthChange onHealthChange;

    public delegate void OnDeath();
    public OnDeath death;



    // Start is called before the first frame update
    private void Start()
    {
        currentHealth = setHealth;
        onHealthChange(currentHealth, setHealth);
    }

    // Update is called once per frame
    private void Update()
    {
        if (currentHealth <= 0 && !dead)
        {
            dead = true;
            KillPlayer();
        }       
    }

    // This will get called by anything which needs to damage the player
    public void TakeDamage(int _damage)
    {
        Debug.Log("oof I took " + _damage + " damage");
        currentHealth = Mathf.Clamp(currentHealth -= _damage, 0, setHealth);
        onHealthChange(currentHealth, setHealth);
    }

    private void KillPlayer()
    {
        if(dead)
        {
            Time.timeScale = 0;
            death();
            Debug.Log("i ded");
            //Restart scene or something
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.tag == "DamageBox")
        //{
        //    StartCoroutine("TakeDamage");
        //}

        if (other.gameObject.tag == "HealthPotion")
        {
            RestoreHealth();
        }
    }

    private void RestoreHealth()
    {
        Debug.Log("Healing-yay");
        currentHealth += healthRestored;
        currentHealth = Mathf.Clamp(currentHealth, 0, setHealth);
        onHealthChange(currentHealth, setHealth);
    }
}
