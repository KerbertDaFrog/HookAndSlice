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

    [SerializeField]
    private KnockbackOnCollision knockBack;

    public bool meteorLanded;

    private MeteorDamage meteor;

    private void OnEnable()
    {
        currentLifeTime = setLifeTime;
        StartCoroutine("TurnMeteorLandedBoolOnAndOff");
    }

    private void Awake()
    {
        meteor.enabled = false;
        gameObject.SetActive(false);
    }

    private void Start()
    {
        kb = FindObjectOfType<KnightBoss>();
        GetComponent<SphereCollider>().radius = radius;
    }

    private void Update()
    {
        currentLifeTime = Mathf.Clamp(currentLifeTime -= Time.deltaTime, 0f, setLifeTime);

        if (currentLifeTime <= 0)
        {
            gameObject.SetActive(false);
        }

        if (meteorLanded == true)
        {
            knockBack.enabled = true;
            Debug.Log("Knockback turning on");
        }
        else if (meteorLanded == false)
        {
            knockBack.enabled = false;
        }
    }

    IEnumerator TurnMeteorLandedBoolOnAndOff()
    {
        yield return new WaitForSeconds(0.6f);
        meteor.enabled = true;
        meteorLanded = true;
        yield return new WaitForSeconds(0.3f);
        meteor.enabled = false;
        meteorLanded = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(meteorLanded == true)
            {
                other.gameObject.GetComponentInParent<PlayerHealth>().TakeDamage(kb.Damage());
            }
        }
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
