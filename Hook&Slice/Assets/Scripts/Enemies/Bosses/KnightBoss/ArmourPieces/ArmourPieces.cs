using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourPieces : HookableObjects
{
    private KnightBoss kb;

    public GameObject[] armourPieces = new GameObject[5];

    private void Start()
    {
        kb = GetComponentInParent<KnightBoss>();
    }

    private void Update()
    {
        if(hooked)
        {
            StartCoroutine(FallOffKnightBoss());
        }
    }

    IEnumerator FallOffKnightBoss()
    {
        yield return null;
        //play sound
        yield return null;
        Destroy(gameObject);
        yield return null; 
        //play armour falling animation
        yield return new WaitForSeconds(0.5f);
        kb.SetState(Enemy.EnemyStates.frenzy);
        //tell knight to go to frenzy state
    }
}
