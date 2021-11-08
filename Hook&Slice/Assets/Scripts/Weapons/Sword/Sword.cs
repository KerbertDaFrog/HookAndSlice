using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField]
    private Hook hook;

    public bool swinging;

    public bool done;

    [SerializeField]
    private int damage;

    [SerializeField]
    private Animator anim;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
            Invoke("GetHook", 0.1f);
 
        if(Input.GetMouseButtonDown(1))
        {
            if(!swinging)
            {
                done = false;
                swinging = true;
                anim.SetBool("swing", true);
                FindObjectOfType<AudioManager>().Play("SwordSwing");
                StartCoroutine("SwingDone");
            }
        }
    }

    IEnumerator SwingDone()
    {
        if (hook && hook.retracted)
        {
            if (hook.enemies != null)
            {
                for (int i = hook.enemies.Count - 1; i >= 0; i--)
                {
                    hook.enemies[i].SetState(Enemy.EnemyStates.dead);
                }
            }
        }
        else 
        if(hook == null)
        {
            RaycastHit hit;

            int enemyLayerMask = 1 << 11;

            float rayLength = 5f;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayLength, enemyLayerMask))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log("Did Hit Enemy");
                hit.transform.gameObject.GetComponentInParent<EnemyHealth>().TakeDamage(damage);

                float knockbackStrength = 3f;

                Rigidbody rb = hit.transform.gameObject.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    Vector3 dir = hit.transform.position - transform.position;
                    dir.y = 0;

                    rb.AddForce(dir.normalized * knockbackStrength, ForceMode.Impulse);
                }
            }
            else
            {
                Debug.Log("Did Not Hit Enemy");
            }
        }
        yield return new WaitForSeconds(0.2f);
        swinging = false;
        anim.SetBool("swing", false);
        done = true;
    }

    void GetHook()
    {
        hook = FindObjectOfType<Hook>();
    }
}
