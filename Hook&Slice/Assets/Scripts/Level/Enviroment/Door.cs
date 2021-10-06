using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    [SerializeField]
    private GameObject interactIcon;

    private bool interact = false;
    //private bool open = false;



    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void InteractON()
    {
        interact = true;
        interactIcon.SetActive(true);
    }

}
