using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject healthPotion;

    [SerializeField]
    private float currentRespawnTimer;

    [SerializeField]
    private float setRespawnTimer;

    [SerializeField]
    private bool healthPotionExists;

    private void Start()
    {
        currentRespawnTimer = setRespawnTimer;
    }

    private void Update()
    {
        if(healthPotionExists == false)
        {
            currentRespawnTimer = Mathf.Clamp(currentRespawnTimer -= Time.deltaTime, 0f, setRespawnTimer);
        }    

        if(currentRespawnTimer <= 0 && healthPotionExists == false)
        {
            Instantiate(healthPotion, gameObject.transform.position, Quaternion.identity);
            currentRespawnTimer = setRespawnTimer;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "HealthPotion")
        {
            healthPotionExists = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "HealthPotion")
        {
            healthPotionExists = false;
        }
    }
}
