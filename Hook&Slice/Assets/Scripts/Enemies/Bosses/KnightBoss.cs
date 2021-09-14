using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightBoss : Enemy
{
    public enum KnightState
    {
        idle,
        staggered,
        attacking,
        dead
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public override void Update()
    {
        
    }

    protected override void Attack()
    {
        attacking = true;
        
        base.Attack();
    }

    public void SetState(KnightState state)
    {
        switch(state)
        {
            case KnightState.idle:
                //idle
                break;
            case KnightState.staggered:
                //staggered
                break;
            case KnightState.attacking:
                Attack();
                break;
            case KnightState.dead:
                //dead
                break;
        }
    }
}
