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
    private GameObject[] meteorDamage = new GameObject[3];

    [Header("Summoning")]
    [SerializeField]
    private GameObject[] summonedEnemies = new GameObject[2];

    [SerializeField]
    private Transform[] summonSpawns = new Transform[3];

    [Header("Attack Cooldown")]
    [SerializeField]
    private float currentAttackCooldown;
    [SerializeField]
    private float setAttackCooldown = 5f;

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
        currentAttackCooldown = 5f;
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

        if(Input.GetKeyDown(KeyCode.K))
        {
            SetState(EnemyStates.staggered);
        }

        if (currentAttackState != AttackState.nil)
        {

        }

        if(setAttackCooldown > 0f)
        {
            if(currentState == EnemyStates.seen)
            {
                currentAttackCooldown = Mathf.Clamp(currentAttackCooldown -= Time.deltaTime, 0f, setAttackCooldown);
            }       
        }
    }

    #region EnemyBehaviour
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
                if(currentAttackCooldown <= 0)
                {
                    SetState(EnemyStates.attacking);
                }
            }
            else if (currentState == EnemyStates.attacking)
            {
                if(currentAttackCooldown <= 0)
                {
                    int randAttackState = Random.Range(0, 3);
                    if (randAttackState == 0)
                    {
                        if(currentAttackState != AttackState.slam && currentAttackState != AttackState.range && currentAttackState != AttackState.summon)
                        {
                            SetAttackState(AttackState.slam);
                        }
                    }
                    else if (randAttackState == 1)
                    {
                        if (currentAttackState != AttackState.slam && currentAttackState != AttackState.range && currentAttackState != AttackState.summon)
                        {
                            SetAttackState(AttackState.range);
                        }                      
                    }
                    else if (randAttackState == 2)
                    {
                        if (currentAttackState != AttackState.slam && currentAttackState != AttackState.range && currentAttackState != AttackState.summon)
                        {
                            SetAttackState(AttackState.summon);
                        }          
                    }
                }
                else if(currentAttackState == AttackState.nil)
                {
                    if(currentState != EnemyStates.frenzy)
                    {
                        SetState(EnemyStates.seen);
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
    #endregion

    IEnumerator Frenzy()
    {
        SetAttackState(AttackState.slam);
        Debug.Log("Doing Slam");
        while (currentAttackState == AttackState.slam && currentState != EnemyStates.dead)
        {
            yield return null;
        }
        SetAttackState(AttackState.range);
        Debug.Log("Doing Range");
        while (currentAttackState == AttackState.range && currentState != EnemyStates.dead)
        {
            yield return null;
        }
        SetAttackState(AttackState.summon);
        Debug.Log("Doing Summon");
        while (currentAttackState == AttackState.summon && currentState != EnemyStates.dead)
        {
            yield return null;
        }
        SetState(EnemyStates.staggered);
        Debug.Log("Frenzy Done");
    }

    #region Attacks
    IEnumerator SlamAttack()
    {
        currentAttackState = AttackState.slam;
        yield return null;
        anim.SetBool("slash", true);
        yield return new WaitForSeconds(0.3f);
        Instantiate(shockWav, shockWavSpawns[0].transform.position, shockWavSpawns[0].transform.rotation);
        Instantiate(shockWav, shockWavSpawns[1].transform.position, shockWavSpawns[1].transform.rotation);
        Instantiate(shockWav, shockWavSpawns[2].transform.position, shockWavSpawns[2].transform.rotation);
        Instantiate(shockWav, shockWavSpawns[3].transform.position, shockWavSpawns[3].transform.rotation);
        yield return new WaitForSeconds(0.5f);
        Instantiate(shockWav, shockWavSpawns[0].transform.position, shockWavSpawns[0].transform.rotation);
        Instantiate(shockWav, shockWavSpawns[1].transform.position, shockWavSpawns[1].transform.rotation);
        Instantiate(shockWav, shockWavSpawns[2].transform.position, shockWavSpawns[2].transform.rotation);
        Instantiate(shockWav, shockWavSpawns[3].transform.position, shockWavSpawns[3].transform.rotation);
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("slash", false);      
        yield return null;    
        if (currentState != EnemyStates.frenzy)
        {
            currentAttackCooldown = setAttackCooldown;
        }
        currentAttackState = AttackState.nil;
    }

    IEnumerator RangeAttack()
    {
        currentAttackState = AttackState.range;
        yield return null;
        //turn range attack animation on
        anim.SetBool("slam", true);
        yield return new WaitForSeconds(0.5f);
        meteorDamage[0].SetActive(true);
        meteorDamage[1].SetActive(true);
        meteorDamage[2].SetActive(true);
        meteorDamage[3].SetActive(true);
        meteorDamage[4].SetActive(true);
        anim.SetBool("slam", false);
        yield return new WaitForSeconds(4f);
        if (currentState != EnemyStates.frenzy)
        {
            currentAttackCooldown = setAttackCooldown;
        }
        currentAttackState = AttackState.nil;
    }

    IEnumerator SummonMinions()
    {
        currentAttackState = AttackState.summon;
        //random meteor locations
        yield return null;
        anim.SetBool("tap", true);
        yield return new WaitForSeconds(0.5f);
        //do check to see if enemy limit isn't reached
        yield return null;
        Instantiate(summonedEnemies[0], summonSpawns[0].transform.position, summonSpawns[0].transform.rotation);
        Instantiate(summonedEnemies[1], summonSpawns[1].transform.position, summonSpawns[1].transform.rotation);
        Instantiate(summonedEnemies[0], summonSpawns[2].transform.position, summonSpawns[2].transform.rotation);
        yield return null;
        anim.SetBool("tap", false);
        yield return null;
        if (currentState != EnemyStates.frenzy)
        {
            currentAttackCooldown = setAttackCooldown;
        }
        currentAttackState = AttackState.nil;
    }
    #endregion

    #region State Operations
    public void SetAttackState(AttackState state)
    {
        Debug.Log("<color=green>" + gameObject.name + " is going from " + currentState + " to " + state + "</color>");
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

    protected override void GoToStaggeredState()
    {
        currentState = EnemyStates.staggered;
        anim.SetBool("stagger", true);
        Debug.Log("Staggered");
    }

    protected override void GoToFrenzyState()
    {
        base.GoToFrenzyState();
        anim.SetBool("stagger", false);
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
