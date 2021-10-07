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
    private Animator anim;

    private Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        currentHealth = setHealth;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, setHealth);

        hook = FindObjectOfType<Hook>();

        //if (currentHealth <= 0)
        //    enemy.SetState(Enemy.EnemyStates.dead);

        if (hooked && currentHealth <= 0 || currentHealth <= 0)
            enemy.SetState(Enemy.EnemyStates.dead);

        if (transform.parent == null)
            hooked = false;

        if(hooked != false)
            anim.SetBool("caught", true);
        else if (hooked != true)
            anim.SetBool("caught", false);
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
