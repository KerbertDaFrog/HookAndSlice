using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLocked : MonoBehaviour
{
    [SerializeField]
    private Animator door;

    private void OnTriggerEnter(Collider other)
    {
        door.SetBool("doorClose", true);
        door.SetBool("doorOpen", false);
        Destroy(gameObject);
    }

}
