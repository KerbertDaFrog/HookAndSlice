using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : Enemy
{
    [Header("Fireball")]
    [SerializeField]
    private GameObject fireballEmmiter;

    [SerializeField]
    private GameObject fireball;

    [SerializeField]
    private float fireballForce;

    private bool attack;

    private bool alreadyAttacked;


    protected override void GoToAttackState()
    {
        base.GoToAttackState();
        attack = true;
        //StartCoroutine(FireballDelay());

    }

    protected override void LeaveAttackState()
    {
        base.LeaveAttackState();
        attack = false;
    }

    //IEnumerator FireballDelay()
    //{
    //    if(attack == true)
    //    {
    //        //GameObject TemporaryFireballHolder;
    //        //TemporaryFireballHolder = Instantiate(fireball, fireballEmmiter.transform.position, fireballEmmiter.transform.rotation) as GameObject;

    //        //Rigidbody TemporaryRidgidbody;
    //        //TemporaryRidgidbody = TemporaryFireballHolder.GetComponent<Rigidbody>();

    //        //TemporaryRidgidbody.AddForce(transform.forward * fireballForce);

    //        //Destroy(TemporaryFireballHolder, 5f);
    //        yield return new WaitForSeconds(1f);
    //        if(!alreadyAttacked)
    //        {
    //            alreadyAttacked = true;
    //            Instantiate(fireball, fireballEmmiter.transform.position, fireballEmmiter.transform.rotation);
    //            yield return new WaitForSeconds(4f);
    //            alreadyAttacked = false;

    //        }

    //    }

    //}

}
