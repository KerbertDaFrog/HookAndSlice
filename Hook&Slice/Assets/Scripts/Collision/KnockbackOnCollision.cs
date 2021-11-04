using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackOnCollision : MonoBehaviour
{
    [SerializeField]
    private float knockbackStrength;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Vector3 dir = other.transform.position - transform.position;
                dir.y = 0;

                rb.AddForce(dir.normalized * knockbackStrength, ForceMode.Impulse);
            }
        }      
    }
}
