using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int setHealth;
    [SerializeField]
    private int currentHealth;
    [SerializeField]
    private int damageTaken;

    [SerializeField]
    private bool dead;

    [SerializeField]
    private GameObject deathScreen;

    [SerializeField]
    private Hook hook;

    // Start is called before the first frame update
    private void Start()
    {
        currentHealth = setHealth;
    }

    // Update is called once per frame
    private void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, setHealth);

        if (currentHealth <= 0)
        {
            dead = true;
            KillPlayer();
        }       
    }

    private void TakeDamage()
    {
        Debug.Log("oof");
        currentHealth -= damageTaken;
    }

    private void KillPlayer()
    {
        if(dead)
        {
            //Time.timeScale = 0;
            //deathScreen.SetActive(true);
            Debug.Log("i ded");
            //Restart scene or something
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "DamageBox")
            TakeDamage();
    }
}
