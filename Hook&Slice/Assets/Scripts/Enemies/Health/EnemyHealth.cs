using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int setHealth;
    [SerializeField]
    private int currentHealth;

    [SerializeField]
    private bool hooked;

    [SerializeField]
    private Hook hook;

    [SerializeField]
    private Sword sword;

    [SerializeField]
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = setHealth;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, setHealth);

        //if (currentHealth <= 0)
        //    StartCoroutine("KillEnemy");

        hook = FindObjectOfType<Hook>();

        sword = FindObjectOfType<Sword>();

        if(hooked && sword.swinging)
            StartCoroutine("KillEnemy");

        if (transform.parent == null)
            hooked = false;

        if(hooked != false)
            anim.SetBool("caught", true);
        else if (hooked != true)
            anim.SetBool("caught", false);
    }

    IEnumerator KillEnemy()
    {
        anim.SetBool("dead", true);
        yield return new WaitForSeconds(2f);
    }

    //IEnumerator TakeDamage()
    //{
    //    while(hooked)
    //    {
    //        currentHealth -= hook.damage;
    //        yield return new WaitForSeconds(1f);
    //    }        
    //}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Hook" && !hooked)
        {
            hooked = true;
            //StartCoroutine("TakeDamage");
        }
    }
}
