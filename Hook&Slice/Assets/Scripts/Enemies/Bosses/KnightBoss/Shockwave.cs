using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    private KnightBoss damage;

    private void Start()
    {
        damage = GetComponent<KnightBoss>();
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponentInParent<PlayerHealth>().TakeDamage(damage.Damage());
        }
    }
}
