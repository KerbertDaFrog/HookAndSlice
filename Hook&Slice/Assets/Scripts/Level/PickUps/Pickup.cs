using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Pickup : MonoBehaviour
{
    [SerializeField]
    private string pickupType;

    public delegate void ConfirmHUD(string pickupType);
    public ConfirmHUD confirmHUD;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            confirmHUD(pickupType);
            Result();
            Destroy(gameObject);
        }
    }

    public virtual void Result()
    {
        //Debug.Log("things are supposed to happen here");
    }

}
