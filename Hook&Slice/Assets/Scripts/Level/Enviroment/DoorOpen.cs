using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class DoorOpen : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    //is the door able to be opened? 
    [SerializeField] 
    private bool unlocked;

    //Things that need to turn off when door is open
    [SerializeField]
    private GameObject doorLight;
    [SerializeField]
    private BoxCollider doorTrigger;

    //the quick bodging of enemies being blocked by doors AKA: enemies are off untill door is open
    [SerializeField]
    private GameObject roomsEnemies;

    private bool interact = false;
    [SerializeField]
    private bool open = false;

    [Header("Analytics:")]
    [SerializeField]
    private int roomNumber;

    //UI systems:
    public delegate void Interaction(bool interact);
    public Interaction interaction;

    public delegate void DoorLocked(bool lockeddoor);
    public DoorLocked doorLocked;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && interact == true)
        {

            if (unlocked)
            {
                anim.SetBool("doorOpen", true);              
                open = true;
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Dungeon", "room: " + roomNumber);
                HudControl.Instance.ProgressByRoom(roomNumber);
                roomsEnemies.SetActive(true);
                Destroy(doorLight);
                Destroy(doorTrigger);
                InteractOFF();
            }
        }
    }

    //if player enters the Trigger eitherside of the door the player is counted as "in-range"
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && unlocked && !open)
        {
            InteractON();
        }
        else if (other.gameObject.tag == "Player" && !open)
        {
            doorLocked(true);
        }
        
    }

    public virtual void InteractON()
    {
        interact = true;
        //interactIcon.SetActive(true);
        interaction(interact);
    }

    //Upon leaving the tirgger the player is no longer in range of interacting
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            InteractOFF();
        }
        
    }

    private void InteractOFF()
    {
        interact = false;
        doorLocked(interact);
        interaction(interact);
    }

    public void UnLock()
    {
        unlocked = true;
    }


}
