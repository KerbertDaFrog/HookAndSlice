using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightBoss : Enemy
{
    public List<GameObject> armourPieces = new List<GameObject>();

    //[Header("ArmourPieces")]
    //public GameObject[] armourPieces { get; set; }

    private int shockWavDam;

    public int armourAmount;

    public enum AttackState
    {
        nil,
        slam,
        range,
        summon
    }

    private AttackState currentAttackState;

    protected override void Start()
    {
        //armourAmount = armourPieces.Length;
        
        base.Start();
    }

    #region ArmourPieces
    //private GameObject[] ArmourPieces
    //{
    //    get { return armourPieces; }
    //    set
    //    {
    //        if (armourPieces == null)
    //        {
    //            armourPieces = value;
    //            return;
    //        }

    //        if (armourPieces.Length > value.Length)
    //        {
    //            //A GameObject has been destroyed

    //            //Check which GameObject has been destroyed
    //            for(int i = 0; i < armourPieces.Length; ++i)
    //            {
    //                for(int j = 0; j < value.Length; ++j)
    //                {
    //                    //Check if GameObject is in the new array
    //                    if(armourPieces[i].GetInstanceID() == value[j].GetInstanceID())
    //                    {
    //                        //GameObject still here
    //                        continue;
    //                    }
    //                }
    //                //If this part of the code is reached, a destroyed GameObject has been found
    //                Debug.Log("The GameObject #" + i + "has been destroyed!");
    //            }
    //        }
    //        else if(armourPieces.Length < value.Length)
    //        {
    //            //A GameObject has been added
    //        }
    //        armourPieces = value;
    //    }   
    //}
    #endregion

    protected override void Update()
    {
        base.Update();
    }

    protected override void EnemyBehaviour()
    {
        base.EnemyBehaviour();
    }

    public int ShockwaveDamage()
    {
        return shockWavDam;
    }

    protected void Attack()
    {

    }

    IEnumerator SlamAttack()
    {
        currentAttackState = AttackState.slam;
        yield return new WaitForSeconds(0.1f);
        //slamattack animation
        //Instantiate shockwave
    }

    private void RangeAttack()
    {
        currentAttackState = AttackState.range;
        //rangeattack animation
    }

    private void SummonMinions()
    {
        currentAttackState = AttackState.summon;
        
        //summon animation
    }

    protected override void GoToFrenzyState()
    {
        base.GoToFrenzyState();
        StartCoroutine("Frenzy");
    }

    IEnumerator Frenzy()
    {
        StartCoroutine(SlamAttack());
        while (currentAttackState == AttackState.slam && currentState != EnemyStates.dead)
        {
            yield return null;
        }
        RangeAttack();
        while(currentAttackState == AttackState.range && currentState != EnemyStates.dead)
        {
            yield return null;
        }
        SummonMinions();
        while(currentAttackState == AttackState.summon && currentState != EnemyStates.dead)
        {
            yield return null;
        }
    }
}
