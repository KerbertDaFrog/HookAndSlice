using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSecret : MonoBehaviour
{

    [SerializeField]
    private Animator anim;

    private bool interact = false;
    private bool open = false;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)&& interact == true)
        {
            anim.SetBool("doorOpen", true);
            open = true;
            interact = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !open)
        {
            interact = true;        
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            interact = false;
        }
    }


}
