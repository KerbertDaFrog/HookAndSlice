using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourPieces : HookableObjects
{
    private KnightBoss kb;

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
        Debug.Log("Knight Boss Going to Frenzy");
        kb.SetState(Enemy.EnemyStates.frenzy);
        //tell knight to go to frenzy state
    }
}
