using System.Collections;
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

    [SerializeField]
    private GameObject doorLight;

    [SerializeField]
    private BoxCollider doorTrigger;


    //the quick bodging of enemies being blocked by doors AKA: enemies are off untill door is open
    [SerializeField]
    private GameObject roomsEnemies;

    private bool interact = false;
    private bool open = false;

    public delegate void Interaction(bool interact);
    public Interaction interaction;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && interact == true)
        {

            if (unlocked)
            {
                anim.SetBool("doorOpen", true);              
                open = true;
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
            locked.SetActive(true);
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
        //interactIcon.SetActive(false);
        locked.SetActive(false);
        interaction(interact);
    }

    public void UnLock()
    {
        unlocked = true;
    }


}
