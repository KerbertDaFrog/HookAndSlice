using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField]
    private Hook hook;

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

                if(hook && hook.retracted)
                {
                    for (int i = hook.enemies.Count - 1; i >= 0; i--)
                    {
                        Destroy(hook.enemies[i].gameObject);
                        
                        hook.enemies.Remove(hook.enemies[i]);
                    }
                    
                }               
                Invoke("SwingDone", 1f);
                anim.SetBool("swing", true);
                //Play Animation
                //Deal Damage to Enemies
            }
        }
    }

    void SwingDone()
    {
        for (int i = hook.enemies.Count - 1; i >= 0; i--)

        {

            Destroy(hook.enemies[i].gameObject);

            hook.enemies.Remove(hook.enemies[i]);

        }
        swinging = false;
        anim.SetBool("swing", false);
    }

    void GetHook()
    {
        hook = FindObjectOfType<Hook>();
    }
}
