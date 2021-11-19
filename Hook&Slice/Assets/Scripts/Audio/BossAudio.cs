using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAudio : MonoBehaviour
{
    //[SerializeField]
    //private AudioSource swing;

    [SerializeField]
    private AudioSource slamWindup;

    [SerializeField]
    private AudioSource slam;

    [SerializeField]
    private AudioSource death;

    public void SwingSound()
    {
        //swing.Play();
    }

    public void SlamWindup()
    {
        slamWindup.Play();
    }

    public void SlamSound()
    {
        slam.Play();
    }

    public void DeathSound()
    {
        death.Play();
    }

}
