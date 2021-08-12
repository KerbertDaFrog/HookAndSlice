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

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = setHealth;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, setHealth);

        if (currentHealth <= 0)
            KillEnemy();

        hook = FindObjectOfType<Hook>();

        if (transform.parent == null)
            hooked = false;
    }

    private void KillEnemy()
    {
        Destroy(gameObject);
    }

    IEnumerator TakeDamage()
    {
        while(hooked)
        {
            currentHealth -= hook.damage;
            yield return new WaitForSeconds(1f);
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Hook" && !hooked)
        {
            hooked = true;
            StartCoroutine("TakeDamage");
        }
    }
}
