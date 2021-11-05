using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimationStart : MonoBehaviour
{

    private void Awake()
    {
        Animator anim = GetComponent<Animator>();
        //AnimationStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
        //anim.Play(state.fullPathHash, -1, Random.Range(0f, 1f));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
