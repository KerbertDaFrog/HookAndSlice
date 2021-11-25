using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    private bool open;

    private bool playerinside;
    private bool healthpotion;

    [SerializeField]
    private GameObject healthItem;
    [SerializeField]
    private GameObject potionSpawn;

    public void DoorOpen()
    {
        anim.SetBool("doorOpen", true);
    }



    private void OnTriggerEnter(Collider other)
    {
        playerinside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerinside = false;
            anim.SetBool("doorClose", true);
        }
    }
}
