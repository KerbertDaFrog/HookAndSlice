using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookableObjects : MonoBehaviour
{
    protected bool hooked;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Hook" && !hooked)
        {
            hooked = true;
        }
    }
}
