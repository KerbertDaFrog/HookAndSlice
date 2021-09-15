using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightBoss : Enemy
{

    private List<GameObject> armorPieces = new List<GameObject>();

    private bool staggered;
    private bool frenzied;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
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
