using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourPieces : HookableObjects
{
    [SerializeField]
    protected KnightBoss kb;

    private void Awake()
    {
        kb = GetComponentInParent<KnightBoss>();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Hook")
        {
            if(hooked == false)
            {
                if(kb.currentState == Enemy.EnemyStates.staggered)
                {
                    Debug.Log("Hooked");
                    hooked = true;
                }
            }          
        }
    }
}
