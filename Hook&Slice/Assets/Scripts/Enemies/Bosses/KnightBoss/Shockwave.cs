using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    [SerializeField]
    private KnightBoss kb;

    private float lifeTime = 1.5f;

    private float speed = 10f;

    private void Start()
    {
        kb = GetComponentInParent<KnightBoss>();
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

    private void ForceFeedbackOnPlayer()
    {

    }
}
