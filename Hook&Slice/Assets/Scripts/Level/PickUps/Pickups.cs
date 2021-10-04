using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{

    [Header("Collection")]
    [SerializeField]
    private HudControl HC;

    [SerializeField]
    private Text confirmationText;


    [Header("Type Of Pickup")]
    [SerializeField]
    private bool healthPickup;



    private void OnTriggerEnter(Collider other)
    {
        //hudCollectIcon.SetActive(true);
        Result();
        Destroy(gameObject);
    }

    private void Result()
    {
        if (healthPickup)
        {
            HC.CollectionON();
            confirmationText.text = "Collected: " + "Health";
        }
    }

}
