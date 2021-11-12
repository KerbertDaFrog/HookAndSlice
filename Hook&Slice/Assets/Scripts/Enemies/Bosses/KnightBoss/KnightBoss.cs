using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightBoss : Enemy
{
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
    private GameObject meteorDamage;
    public bool meteorLanded = false;

    [Header("Summoning")]
    [SerializeField]
    private Transform[] summonSpawns = new Transform[3];

    [SerializeField]
    private float attackCooldown;

    [SerializeField]
    private float setAttackCooldown;

    public enum AttackState
    {
        nil,
        slam,
        range,
        summon
    }

    public AttackState currentAttackState;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        //press to test shockwave spawn
        if(Input.GetKeyDown(KeyCode.L))
        {
            Instantiate(shockWav, shockWavSpawns[0].transform.position, shockWavSpawns[0].transform.rotation);
            Instantiate(shockWav, shockWavSpawns[1].transform.position, shockWavSpawns[1].transform.rotation);
            Instantiate(shockWav, shockWavSpawns[2].transform.position, shockWavSpawns[2].transform.rotation);
            Instantiate(shockWav, shockWavSpawns[3].transform.position, shockWavSpawns[3].transform.rotation);
        }

        //press to test meteor spawn
        if(Input.GetKeyDown(KeyCode.K))
        {

        }

        if(attackCooldown <= 0)
        {
            //do attack things
        }
        
        if(attackCooldown > 0)
        {
            attackCooldown = Mathf.Clamp(attackCooldown -= Time.deltaTime, 0f, setAttackCooldown);
        }       

        //SetAttackState(AttackState.slam);
    }

    protected override void EnemyBehaviour()
    {
        base.EnemyBehaviour();
    }

    public int ShockwaveDamage()
    {
        return shockWavDam;
    }

    public void SetAttackState(AttackState state)
    {
        switch(state)
        {
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
    }

    IEnumerator RangeAttack()
    {
        currentAttackState = AttackState.range;
        yield return null;
        //turn range attack animation on
        yield return new WaitForSeconds(3f);
        //turn range attack animation off
        meteorLanded = true;
        meteorDamage.SetActive(true);
        yield return new WaitForSeconds(1f);
        meteorDamage.SetActive(false);
        meteorLanded = false;
    }

    IEnumerator SummonMinions()
    {
        currentAttackState = AttackState.summon;
        yield return null;
        //turn summon animation on
        yield return new WaitForSeconds(1f);
        //do check to see if enemy limit isn't reached
        yield return null;
        Instantiate(shockWav, summonSpawns[0].transform.position, summonSpawns[0].transform.rotation);
        Instantiate(shockWav, summonSpawns[1].transform.position, summonSpawns[1].transform.rotation);
        Instantiate(shockWav, summonSpawns[2].transform.position, summonSpawns[2].transform.rotation);
        //instantiate enemies
        yield return new WaitForSeconds(1f);
        //turn summon animation off
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
