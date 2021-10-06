using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Pickup
{
    //[SerializeField]
    //private PlayerHealth ph;

    public override void Result()
    {
        Debug.Log("Health gets added here");
    }

}
