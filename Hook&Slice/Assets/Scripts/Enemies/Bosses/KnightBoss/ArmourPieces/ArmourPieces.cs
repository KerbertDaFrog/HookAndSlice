using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourPieces : HookableObjects
{
    [SerializeField]
    protected KnightBoss kb;

    [SerializeField]
    private List<GameObject> armourPieces = new List<GameObject>();

    private void Awake()
    {
        kb = GetComponentInParent<KnightBoss>();
    }

    private void Update()
    {
        for (int i = armourPieces.Count - 1; i >= 0; i--)
        {
            if (armourPieces[i] == null)
            {
                Debug.Log("RemovingArmour");
                armourPieces.Remove(armourPieces[i]);
            }
        }

        if(armourPieces.Count == 0)
        {
            Debug.Log("Setting Boss State to Dead");
            kb.SetState(Enemy.EnemyStates.dead);
        }
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
