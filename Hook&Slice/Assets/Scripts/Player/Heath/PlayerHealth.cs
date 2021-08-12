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
            KillPlayer();
    }

    private void TakeDamage()
    {

    }

    private void KillPlayer()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "DamageBox")
            TakeDamage();
    }
}
