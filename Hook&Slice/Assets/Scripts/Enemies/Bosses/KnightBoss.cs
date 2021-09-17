using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightBoss : Enemy
{

    private List<GameObject> armourPieces = new List<GameObject>();

    private int armourAmount;

    private bool staggered;
    private bool frenzied;

    protected override void Start()
    {
        armourAmount = armourPieces.Count;

        base.Start();
    }

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

    //private GameObject[] ArmourAmount
    //{

    //}
    //private void OnArmourRemoved()
    //{
    //    if()
    //    {
    //        frenzied = true;
    //    }
    //}

    protected override void EnemyBehaviour()
    {
        if (currentState == EnemyStates.frenzy)
        {
            frenzied = true;
            SetState(EnemyStates.frenzy);
        }
            
        if (currentState == EnemyStates.staggered)
            SetState(EnemyStates.staggered);

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
