using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    private KnightBoss damage;

    private float lifeTime = 1.5f;

    private float speed = 10f;

    private void Start()
    {
        damage = GetComponent<KnightBoss>();
    }

    private void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponentInParent<PlayerHealth>().TakeDamage(damage.Damage());
            Destroy(gameObject);
        }
    }
}
