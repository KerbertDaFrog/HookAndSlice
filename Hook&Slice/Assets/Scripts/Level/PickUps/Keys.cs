using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keys : MonoBehaviour
{
    //hud collection confirmation
    //[SerializeField]
    //private GameObject hudCollectIcon;

    [SerializeField]
    private HudControl HC;

    [SerializeField]
    private Text confirmationText;

    [SerializeField]
    private string keyType;

    [Header("Doors to Unlock")]

    [SerializeField]
    private DoorOpen door;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            confirmationText.text = "Collected: " + keyType;
            HC.CollectionON();
            //hudCollectIcon.SetActive(true);
            door.UnLock();
            Destroy(gameObject);
        }
    }
}
