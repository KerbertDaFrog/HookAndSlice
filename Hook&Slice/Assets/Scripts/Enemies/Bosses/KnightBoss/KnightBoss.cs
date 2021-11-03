using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightBoss : Enemy
{
    [Header("KnightBoss")]
    public List<GameObject> armourPieces = new List<GameObject>();

    public int armourAmount;

    [SerializeField]
    private GameObject shockWav;

    [SerializeField]
    private int shockWavDam;

    [SerializeField]
    private List<Transform> shockWavSpawns = new List<Transform>();

    [SerializeField]
    private int shockWavSpawn = 0;

    [SerializeField]
    private GameObject meteorDamage;

    public bool meteorLanded = false;

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

        shockWavSpawn = shockWavSpawns.Count;
    }

    protected override void Update()
    {
        base.Update();

        //press to test shockwave spawn
        if(Input.GetKeyDown(KeyCode.L))
        {
            Instantiate(shockWav, gameObject.transform, false);
        }
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
        //int index = shockWavSpawns.IndexOf;
        //Instantiate(shockWav, );
        //slamattack animation
        //Instantiate shockwave
    }

    IEnumerator RangeAttack()
    {
        currentAttackState = AttackState.range;
        yield return null;
        //turn range attack animation on
        yield return new WaitForSeconds(3f);
        //turn range attack animation off
        meteorLanded = true;
        yield return new WaitForSeconds(3f);
        meteorLanded = false;
        
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
