using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightBoss : Enemy
{

    private List<GameObject> armourPieces = new List<GameObject>();

    private int maxArmour;

    private bool staggered;
    private bool frenzied;

    // Update is called once per frame
    protected override void Update()
    {
        if (!playerInSightRange && !playerInAttackRange)
            currentState = EnemyStates.idle;

        if (currentState == EnemyStates.frenzy)
            frenzied = true;
        else
            frenzied = false;

        if (playerInSightRange && !playerInAttackRange)
            currentState = EnemyStates.chasing;

        if (playerInSightRange && playerInAttackRange)
            currentState = EnemyStates.attacking;
        else if (!playerInAttackRange)
            attacking = false;

        if (attacking)
            anim.SetBool("attack", true);
        else if (!attacking)
            anim.SetBool("attack", false);

        EnemyBehaviour();
    }

    private void OnArmourRemoved()
    {
        if(armourPieces.Count < armourPieces.Count)
        {
            frenzied = true;
        }
    }

    protected override void EnemyBehaviour()
    {

        base.EnemyBehaviour();
    }

    protected override void Attack()
    {        
        base.Attack();
    }

    private void Frenzy()
    {
        if(frenzied)
        {

        }
    }

    public override void SetState(EnemyStates state)
    {
        switch(state)
        {
            case EnemyStates.idle:
                Idle();
                break;
            case EnemyStates.attacking:
                Attack();
                break;
            case EnemyStates.frenzy:
                Frenzy();
                break;
        }
    }
}
