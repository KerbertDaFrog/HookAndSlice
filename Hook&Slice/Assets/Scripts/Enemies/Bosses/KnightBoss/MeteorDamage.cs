using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorDamage : MonoBehaviour
{
    [SerializeField]
    private KnightBoss kb;

    [SerializeField]
    private float radius = 1.0f;

    [SerializeField]
    private float setLifeTime;

    [SerializeField]
    private float currentLifeTime;

    private void OnEnable()
    {
        currentLifeTime = setLifeTime;
    }

    private void Start()
    {
        kb = FindObjectOfType<KnightBoss>();
        gameObject.SetActive(false);
        GetComponent<SphereCollider>().radius = radius;
    }

    private void Update()
    {
        if(kb.currentAttackState == KnightBoss.AttackState.range)
        {
            if(kb.meteorLanded == true)
            {
                gameObject.SetActive(true);
            }
            //else 
            //if (kb.meteorLanded == false)
            //{
            //    gameObject.SetActive(false);
            //}
        }

        currentLifeTime = Mathf.Clamp(currentLifeTime -= Time.deltaTime, 0f, setLifeTime);

        if (currentLifeTime <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponentInParent<PlayerHealth>().TakeDamage(kb.Damage());
        }
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
