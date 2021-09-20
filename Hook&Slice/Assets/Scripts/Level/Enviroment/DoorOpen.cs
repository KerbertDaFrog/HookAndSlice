﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{

    [SerializeField]
    private Animator anim;

   
    //is the door able to be opened? 
    [SerializeField] 
    private bool unlocked;

    [SerializeField]
    private GameObject interactIcon;

    [SerializeField]
    private GameObject locked;



    private bool interact = false;
    private bool open = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && interact == true)
        {

            if (unlocked)
            {
                anim.SetBool("doorOpen", true);              
                open = true; 
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
            locked.SetActive(true);
        }
    }

    private void InteractON()
    {
        interact = true;
        interactIcon.SetActive(true);
    }

    //Upon leaving the tirgger the player is no longer in range of interacting
    private void OnTriggerExit(Collider other)
    {
        InteractOFF();
    }

    private void InteractOFF()
    {
        interact = false;
        interactIcon.SetActive(false);
        locked.SetActive(false);
    }

    public void UnLock()
    {
        unlocked = true;
    }


}
