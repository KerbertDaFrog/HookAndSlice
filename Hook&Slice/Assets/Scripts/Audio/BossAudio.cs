using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAudio : MonoBehaviour
{
    [SerializeField]
    private AudioSource swing;

    
    private void SwingSound()
    {
        swing.Play();
    }



}
