using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAttack : MonoBehaviour
{
    [Header("Fireball")]
    [SerializeField]
    private GameObject fireballEmmiter;

    [SerializeField]
    private GameObject fireball;

    [SerializeField]
    private float fireballForce;

    public void SpawnFireball()
    {
        Instantiate(fireball, fireballEmmiter.transform.position, fireballEmmiter.transform.rotation);
    }


}
