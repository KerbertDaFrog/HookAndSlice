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

    public int GetHealth()
    {
        return currentHealth;
    }


    // This needs to get called by the sword when the player attacks
    public void TakeDamage(int incomingDamage)
    {
        currentHealth = Mathf.Clamp(currentHealth -= incomingDamage, 0, setHealth);
        if(currentHealth == 0)
            enemy.SetState(Enemy.EnemyStates.dead);
    }

    public void ClearHook()
    {
        hook = null;
        hooked = false;
        transform.SetParent(null);
    }
}
