using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField]
    private Hook hook;

    [SerializeField]
    private Enemy enemy;

    public bool swinging;

    [SerializeField]
    private float damage;

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
                swinging = true;
                anim.SetBool("swing", true);
                FindObjectOfType<AudioManager>().Play("SwordSwing");
                StartCoroutine("SwingDone");
                //Play Animation
                //Deal Damage to Enemies
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
        yield return new WaitForSeconds(0.4f);
        swinging = false;
        anim.SetBool("swing", false);
    }

    void GetHook()
    {
        hook = FindObjectOfType<Hook>();
    }
}
