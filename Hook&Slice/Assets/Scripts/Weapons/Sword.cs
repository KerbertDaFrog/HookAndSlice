using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField]
    private Hook hook;

    [SerializeField]
    private bool swinging;

    [SerializeField]
    private float damage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Invoke("GetHook", 0.1f);
        }

        if(Input.GetMouseButtonDown(1))
        {
            if(!swinging)
            {
                swinging = true;
                if(hook)
                {
                    for (int i = hook.enemies.Count - 1; i >= 0; i--)
                    {
                        Destroy(hook.enemies[i].gameObject);
                        hook.enemies.Remove(hook.enemies[i]);
                    }
                }               
                Invoke("SwingDone", 1f);
                //Play Animation
                //Deal Damage to Enemies
            }
        }
    }

    void SwingDone()
    {
        swinging = false;
    }

    void GetHook()
    {
        hook = FindObjectOfType<Hook>();
    }
}
