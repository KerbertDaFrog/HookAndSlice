using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : Enemy
{

    private bool attack;


    protected override void GoToAttackState()
    {
        base.GoToAttackState();
        attack = true;


    }

    protected override void LeaveAttackState()
    {
        base.LeaveAttackState();
        attack = false;
    }

    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(1f);
    }


}
