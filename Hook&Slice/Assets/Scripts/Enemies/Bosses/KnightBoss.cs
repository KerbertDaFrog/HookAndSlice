using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightBoss : Enemy
{
    //private List<GameObject> armourPieces = new List<GameObject>();

    //[Header("ArmourPieces")]
    public GameObject[] armourPieces { get; set; }

    public int armourAmount;

    public enum AttackState
    {
        slam,
        range,
        summon
    }

    private AttackState currentAttackState;

    protected override void Start()
    {
        armourAmount = armourPieces.Length;
        base.Start();
    }

    private GameObject[] ArmourPieces
    {
        get { return armourPieces; }
        set
        {
            if (armourPieces == null)
            {
                armourPieces = value;
                return;
            }

            if (armourPieces.Length > value.Length)
            {
                //A GameObject has been destroyed

                //Check which GameObject has been destroyed
                for(int i = 0; i < armourPieces.Length; ++i)
                {
                    for(int j = 0; j < value.Length; ++j)
                    {
                        //Check if GameObject is in the new array
                        if(armourPieces[i].GetInstanceID() == value[j].GetInstanceID())
                        {
                            //GameObject still here
                            continue;
                        }
                    }
                    //If this part of the code is reached, a destroyed GameObject has been found
                    Debug.Log("The GameObject #" + i + "has been destroyed!");
                }
            }
            else if(armourPieces.Length < value.Length)
            {
                //A GameObject has been added
            }
            armourPieces = value;
        }   
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void EnemyBehaviour()
    {
        base.EnemyBehaviour();
    }

    protected void Attack()
    {

    }

    private void SlamAttack()
    {
        currentAttackState = AttackState.slam;
        //slamattack animation
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
        yield return new WaitForSeconds(1f);
        SlamAttack();

    }
}
