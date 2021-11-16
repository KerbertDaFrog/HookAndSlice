using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightBoss : Enemy
{
    #region Fields
    [Header("Armour")]
    public int armourAmount;

    [Header("ShockWave")]
    [SerializeField]
    private GameObject shockWav;

    [SerializeField]
    private int shockWavDam;

    [SerializeField]
    private Transform[] shockWavSpawns = new Transform[4];

    [Header("Meteors")]
    [SerializeField]
    private Transform[] meteorSpawns = new Transform[3];

    [SerializeField]
    private GameObject meteorDamage;

    public bool meteorLanded = false;

    [Header("Summoning")]
    [SerializeField]
    private GameObject[] summonedEnemies = new GameObject[2];

    [SerializeField]
    private Transform[] summonSpawns = new Transform[3];

    [Header("Attack Cooldown")]
    [SerializeField]
    private float attackCooldown;

    [SerializeField]
    private float setAttackCooldown;

    [Header("Attack States")]
    public AttackState currentAttackState;

    public enum AttackState
    {
        nil,
        slam,
        range,
        summon
    }
    #endregion

    protected override void Start()
    {
        base.Start();
        nav.speed = 0;
    }

    protected override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.L))
        {
            int randAttackState = Random.Range(0, 3);
            if (randAttackState == 0)
            {
                SetAttackState(AttackState.slam);
            }
            else if (randAttackState == 1)
            {
                SetAttackState(AttackState.range);
            }
            else if (randAttackState == 2)
            {
                SetAttackState(AttackState.summon);
            }
            print(randAttackState);
        }

        //press to test shockwave spawn
        if(Input.GetKeyDown(KeyCode.Y))
        {
            SetAttackState(AttackState.slam);
        }

        //press to test meteor spawn
        if (Input.GetKeyDown(KeyCode.U))
        {
            SetAttackState(AttackState.range);
        }

        //press to test summon spawn
        if (Input.GetKeyDown(KeyCode.I))
        {
            SetAttackState(AttackState.summon);
        }
        
        //press to test frenzy MUST NOT BE IN IDLE FOR THIS TO WORK!
        if(Input.GetKeyDown(KeyCode.O))
        {
            SetState(EnemyStates.frenzy);
        }

        if(currentAttackState != AttackState.nil)
        
        if(attackCooldown > 0)
        {
            attackCooldown = Mathf.Clamp(attackCooldown -= Time.deltaTime, 0f, setAttackCooldown);
        }
    }

    protected override void EnemyBehaviour()
    {
        if (currentState != EnemyStates.dead)
        {
            if (currentState == EnemyStates.idle)
            {
                if (playerInSightRange)
                {
                    SetState(EnemyStates.seen);
                }
            }
            else if (currentState == EnemyStates.seen)
            {
                if(attackCooldown <= 0)
                {
                    SetState(EnemyStates.attacking);
                }
            }
            else if (currentState == EnemyStates.attacking)
            {
                if(attackCooldown <= 0)
                {
                    int randAttackState = Random.Range(0, 3);
                    if (randAttackState == 0)
                    {
                        SetAttackState(AttackState.slam);
                    }
                    else if (randAttackState == 1)
                    {
                        SetAttackState(AttackState.range);
                    }
                    else if (randAttackState == 2)
                    {
                        SetAttackState(AttackState.summon);
                    }
                }
            }
            else
            {
                if (!playerInSightRange && !playerInAttackRange && !hasSeenPlayer)
                {
                    SetState(EnemyStates.idle);
                }
            }
        }
    }

    IEnumerator Frenzy()
    {
        SetAttackState(AttackState.slam);
        while (currentAttackState == AttackState.slam && currentState != EnemyStates.dead)
        {
            yield return null;
        }
        SetAttackState(AttackState.range);
        while (currentAttackState == AttackState.range && currentState != EnemyStates.dead)
        {
            yield return null;
        }
        SetAttackState(AttackState.summon);
        while (currentAttackState == AttackState.summon && currentState != EnemyStates.dead)
        {
            yield return null;
        }
    }

    #region Attacks
    IEnumerator SlamAttack()
    {
        currentAttackState = AttackState.slam;
        //turn slamattack animation on
        yield return new WaitForSeconds(0.1f);
        Instantiate(shockWav, shockWavSpawns[0].transform.position, shockWavSpawns[0].transform.rotation);
        Instantiate(shockWav, shockWavSpawns[1].transform.position, shockWavSpawns[1].transform.rotation);
        Instantiate(shockWav, shockWavSpawns[2].transform.position, shockWavSpawns[2].transform.rotation);
        Instantiate(shockWav, shockWavSpawns[3].transform.position, shockWavSpawns[3].transform.rotation);
        yield return new WaitForSeconds(2f);
        //do slam attack again
        yield return new WaitForSeconds(0.5f);
        //turn slamattack animation off
        yield return null;
        if(currentState != EnemyStates.frenzy)
        {
            attackCooldown = setAttackCooldown;
        }
        currentAttackState = AttackState.nil;
    }

    IEnumerator RangeAttack()
    {
        currentAttackState = AttackState.range;
        yield return null;
        //turn range attack animation on
        yield return new WaitForSeconds(3f);
        //turn range attack animation off
        yield return null;
        //Instantiate(meteorDamage, meteorSpawns[0].transform.position, Quaternion.identity);
        //Instantiate(meteorDamage, meteorSpawns[1].transform.position, Quaternion.identity);
        //Instantiate(meteorDamage, meteorSpawns[2].transform.position, Quaternion.identity);
        meteorLanded = true;
        yield return new WaitForSeconds(1f);
        meteorLanded = false;
        yield return null;
        if(currentState != EnemyStates.frenzy)
        {
            attackCooldown = setAttackCooldown;
        }
        currentAttackState = AttackState.nil;
    }

    IEnumerator SummonMinions()
    {
        currentAttackState = AttackState.summon;
        //random meteor locations
        yield return null;
        //turn summon animation on
        yield return new WaitForSeconds(1f);
        //do check to see if enemy limit isn't reached
        yield return null;
        Instantiate(summonedEnemies[0], summonSpawns[0].transform.position, summonSpawns[0].transform.rotation);
        Instantiate(summonedEnemies[1], summonSpawns[1].transform.position, summonSpawns[1].transform.rotation);
        Instantiate(summonedEnemies[0], summonSpawns[2].transform.position, summonSpawns[2].transform.rotation);
        yield return new WaitForSeconds(1f);
        //turn summon animation off
        yield return null;
        if (currentState != EnemyStates.frenzy)
        {
            attackCooldown = setAttackCooldown;
        }
        currentAttackState = AttackState.nil;
    }
    #endregion

    #region State Operations
    public void SetAttackState(AttackState state)
    {
        switch (state)
        {
            case AttackState.nil:
                currentAttackState = AttackState.nil;
                break;

            case AttackState.slam:
                StartCoroutine(SlamAttack());
                break;

            case AttackState.range:
                StartCoroutine(RangeAttack());
                break;

            case AttackState.summon:
                StartCoroutine(SummonMinions());
                break;
        }
    }
    protected override void GoToFrenzyState()
    {
        base.GoToFrenzyState();
        StartCoroutine("Frenzy");
    }
    #endregion

    #region Getters
    public int ShockwaveDamage()
    {
        return shockWavDam;
    }
    #endregion
}
