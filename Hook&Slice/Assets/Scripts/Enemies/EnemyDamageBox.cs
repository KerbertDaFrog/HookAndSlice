using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageBox : MonoBehaviour
{
    [SerializeField] private Enemy myself;
    [SerializeField] bool hitPlayer = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hitPlayer)
        {
            if (other.gameObject.tag == "Player")
            {
                hitPlayer = true;
                other.gameObject.GetComponentInParent<PlayerHealth>().TakeDamage(myself.Damage());
            }
        }
    }

    private void OnDisable()
    {
        hitPlayer = false;
    }


}
