using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourPieces : HookableObjects
{
    private KnightBoss kb;

    private GameObject[] armourPieces = new GameObject[5];

    [SerializeField]
    private Animator anim;

    private void Start()
    {
        kb = GetComponentInParent<KnightBoss>();
    }

    private void Update()
    {
        foreach(GameObject armourPiece in armourPieces)
        {
            if(armourPiece == null)
            {

            }
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Hook")
        {
            if(hooked == false)
            {
                if(kb.currentState != Enemy.EnemyStates.attacking)
                {
                    Debug.Log("Hooked");
                    hooked = true;
                }
            }
        }
    }
}
