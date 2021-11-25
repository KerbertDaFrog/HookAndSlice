﻿using System.Collections;
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
    private GameObject[] meteorDamage = new GameObject[7];

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

    [Header("Booleans")]
    public bool frenzyDone;

    [Header("Integers")]
    [SerializeField]
    private int attacksDone;

    [Header("StaggeredTimer")]
    [SerializeField]
    private float currentStaggeredTimer;
    [SerializeField]
    private float setStaggeredTimer;

    private ParticleSystem ps;
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
        attacksDone = 0;
        frenzyDone = false;
        currentStaggeredTimer = setStaggeredTimer;
        currentAttackCooldown = setAttackCooldown;
        nav.speed = 0;
        ps = GetComponentInChildren<ParticleSystem>();
    }

    protected override void Update()
    {
        base.Update();

        if(currentAttackCooldown > 0f)
        {
            if(currentState == EnemyStates.seen)
            {
                currentAttackCooldown = Mathf.Clamp(currentAttackCooldown -= Time.deltaTime, 0f, setAttackCooldown);
            }       
        }

        if(currentState == EnemyStates.seen)
        {
            if(currentAttackCooldown > 0f)
            {
                currentAttackCooldown = Mathf.Clamp(currentAttackCooldown -= Time.deltaTime, 0f, setAttackCooldown);
            }
        }

        if(currentState == EnemyStates.staggered)
        {
            if(currentAttackCooldown > 0f)
            {
                currentStaggeredTimer = Mathf.Clamp(currentStaggeredTimer -= Time.deltaTime, 0f, setStaggeredTimer);
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
                currentStaggeredTimer = setStaggeredTimer;
                frenzyDone = false;
                if(currentAttackCooldown <= 0)
                {
                    SetState(EnemyStates.attacking);
                }
                else if (attacksDone == 3 && currentState != EnemyStates.frenzy)
                {
                    SetState(EnemyStates.staggered);
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
            else if(currentState == EnemyStates.staggered)
            {           
                if (currentStaggeredTimer <= 0f)
                {
                    if(currentState != EnemyStates.frenzy)
                    {
                        StartCoroutine(SetStateFromStaggeredToSeen());
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

    IEnumerator SetStateFromStaggeredToSeen()
    {
        attacksDone = 0;
        yield return null;
        SetState(EnemyStates.seen);
        yield return null;     
    }

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
        frenzyDone = true;
        yield return null;
        currentStaggeredTimer = setStaggeredTimer;
        yield return null;
        SetState(EnemyStates.staggered);     
        Debug.Log("Frenzy Done");
    }

    #region Attacks
    IEnumerator SlamAttack()
    {
        if(currentAttackState != AttackState.range && currentAttackState != AttackState.summon)
        {
            Debug.Log("Doing Slam");
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
                attacksDone++;
            }
            currentAttackState = AttackState.nil;
        }     
    }

    IEnumerator RangeAttack()
    {
        if(currentAttackState != AttackState.slam && currentAttackState != AttackState.summon)
        {
            Debug.Log("Doing Range");
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
            meteorDamage[5].SetActive(true);
            meteorDamage[6].SetActive(true);
            meteorDamage[7].SetActive(true);
            anim.SetBool("slam", false);
            if(currentState == EnemyStates.frenzy)
            {
                yield return new WaitForSeconds(2f);
            }
            else if (currentState != EnemyStates.frenzy)
            {
                currentAttackCooldown = setAttackCooldown;
                attacksDone++;
            }
            currentAttackState = AttackState.nil;
        }      
    }

    IEnumerator SummonMinions()
    {
        if(currentAttackState != AttackState.slam && currentAttackState != AttackState.range)
        {
            Debug.Log("Doing Summon");
            currentAttackState = AttackState.summon;
            //random meteor locations
            yield return null;
            anim.SetBool("tap", true);
            yield return new WaitForSeconds(0.5f);
            Instantiate(summonedEnemies[0], summonSpawns[0].transform.position, summonSpawns[0].transform.rotation);
            Instantiate(summonedEnemies[1], summonSpawns[1].transform.position, summonSpawns[1].transform.rotation);
            Instantiate(summonedEnemies[0], summonSpawns[2].transform.position, summonSpawns[2].transform.rotation);
            yield return null;
            anim.SetBool("tap", false);
            yield return null;
            if (currentState != EnemyStates.frenzy)
            {
                currentAttackCooldown = setAttackCooldown;
                attacksDone++;
            }
            currentAttackState = AttackState.nil;
        }      
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

    protected override void LeaveStaggeredState()
    {
        anim.SetBool("stagger", false);
    }

    protected override void GoToFrenzyState()
    {
        base.GoToFrenzyState();
        anim.SetBool("stagger", false);
        StartCoroutine("Frenzy");
    }

    protected override void GoToDeadState()
    {
        currentState = EnemyStates.dead;
        this.GetComponent<BoxCollider>().isTrigger = true;
        anim.SetBool("caught", false);
        anim.SetBool("attack", false);
        anim.SetBool("walk", false);
        HudControl.Instance.EnemyKillCount();
        StartCoroutine("Dead");
    }
    #endregion

    protected override IEnumerator Dead()
    {
        ps.Play(true);
        yield return new WaitForSeconds(0.7f);
        ps.Stop();
        anim.SetBool("dead", true);
        yield return new WaitForSeconds(0.7f);
        InstantiateDeadEnemyBody();
        yield return null;
        Destroy(gameObject);
    }

    #region Getters
    public int ShockwaveDamage()
    {
        return shockWavDam;
    }
    #endregion
}
