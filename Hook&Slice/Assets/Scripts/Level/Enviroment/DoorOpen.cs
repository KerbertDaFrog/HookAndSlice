using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{

    [SerializeField]
    private Animator anim;



    private void OnTriggerEnter(Collider other)
    {
        anim.SetTrigger("doorOpen");
    }

}
