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

    [Header("Damage Feedback")]
    [SerializeField]
    private SpriteRenderer sr;
    [SerializeField]
    private Material originalmat;
    [SerializeField]
    private Material damagemat;

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
        StartCoroutine("DamageFeedback");
        if (currentHealth == 0)
        {
            enemy.SetState(Enemy.EnemyStates.dead);
        }
    }

    IEnumerator DamageFeedback()
    {
        sr.material = damagemat;
        yield return new WaitForSeconds(0.1f);
        sr.material = originalmat;
        yield return null;
    }

    public void ClearHook()
    {
        hook = null;
        hooked = false;
        transform.SetParent(null);
    }
}
