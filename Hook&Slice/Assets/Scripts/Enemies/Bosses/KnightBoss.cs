using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightBoss : Enemy
{
    //private List<GameObject> armourPieces = new List<GameObject>();

    //[Header("ArmourPieces")]
    public GameObject[] armourPieces { get; set; }

    public int armourAmount;

    private bool staggered;
    private bool frenzied;

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
        if (!playerInSightRange && !playerInAttackRange)
            currentState = EnemyStates.idle;

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

        if(ArmourPieces.Length == 0)

        EnemyBehaviour();
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
