using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightBoss : Enemy
{
    public enum KnightState
    {
        idle,
        staggered,
        attacking,
        dead
    }

    public KnightState state; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //// Update is called once per frame
    //public override void Update()
    //{
        
    //}

    public void SetState(KnightState _r)
    {

    }
}
