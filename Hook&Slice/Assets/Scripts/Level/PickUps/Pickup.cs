using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    [SerializeField]
    private HudControl HC;

    [SerializeField]
    private Text confirmationText;

    [SerializeField]
    private string pickupType;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            confirmationText.text = "Collected: " + pickupType;
            HC.CollectionON();
            Result();
            Destroy(gameObject);
        }
    }

    public virtual void Result()
    {
        Debug.Log("things happen here");
    }

}
