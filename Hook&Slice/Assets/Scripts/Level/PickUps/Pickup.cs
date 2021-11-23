using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using GameAnalyticsSDK;

public class Pickup : MonoBehaviour
{
    [SerializeField]
    private string pickupType;

    public delegate void ConfirmHUD(string pickupType);
    public ConfirmHUD confirmHUD;

    [SerializeField]
    private bool key = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            HudControl.Instance.CollectionON(pickupType);
            Result();
            if (key)
            {
                GameAnalytics.NewDesignEvent(pickupType);
            }
            Destroy(gameObject);
        }
  
    }

    public virtual void Result()
    {

    }

}
