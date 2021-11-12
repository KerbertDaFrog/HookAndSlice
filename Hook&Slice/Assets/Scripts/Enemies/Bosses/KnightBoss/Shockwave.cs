using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    [SerializeField]
    private KnightBoss kb;

    private float lifeTime = 1.5f;

    private float speed = 10f;

    [SerializeField]
    private float radius = 1.0f;

    private void Start()
    {
        kb = FindObjectOfType<KnightBoss>();
    }

    private void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponentInParent<PlayerHealth>().TakeDamage(kb.ShockwaveDamage());
            Destroy(gameObject);
        }
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
