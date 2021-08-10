using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : MonoBehaviour
{
    //hud collection confirmation
    //[SerializeField]
    //private GameObject hudCollectIcon;

    [SerializeField]
    private DoorOpen dr;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        //hudCollectIcon.SetActive(true);
        dr.UnLock();
        Destroy(gameObject);
    }



}
