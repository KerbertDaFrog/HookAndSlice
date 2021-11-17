using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSecret : MonoBehaviour
{
    private bool interact = false;
    private bool open = false;


    // Update is called once per frame
    void Update()
    {
        if((Input.GetMouseButtonDown(1) && interact == true) || (Input.GetKeyDown(KeyCode.E) && interact == true))
        { 
            open = true;
            interact = false;
            Destroy(gameObject);
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
