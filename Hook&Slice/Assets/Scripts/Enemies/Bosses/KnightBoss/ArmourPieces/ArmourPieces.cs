using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourPieces : HookableObjects
{
    private KnightBoss kb;

    private void Start()
    {

    }

    private void FallOffKnightBoss()
    {
        if(hooked)
        {
            Destroy(gameObject);
            //fall off enemy or sumting
            //tell knight that he's a bit more naked than before
        }
    }
}
