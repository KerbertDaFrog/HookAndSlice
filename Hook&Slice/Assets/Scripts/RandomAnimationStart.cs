using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RandomAnimationStart : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    private void Start()
    {
        Invoke("EnableAnimator", Random.value);
    }

    private void Awake()
    {
        //Animator anim = GetComponent<Animator>();
        //AnimationState state = anim.GetCurrentAnimatorStateInfo(0);
        //anim.Play(state.full, -1, Random.Range(0f, 1f));
        //Animator.Play("Torch", 0, Random.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EnableAnimator()
    {
        anim.enabled = true;
    }

}
