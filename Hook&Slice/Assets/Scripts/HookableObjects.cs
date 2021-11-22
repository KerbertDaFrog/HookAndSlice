using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookableObjects : MonoBehaviour
{
    protected bool hooked;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Hook")
        {
            if(hooked == false)
            {
                Debug.Log("Hooked");
                hooked = true;
            }          
        }
    }
}
