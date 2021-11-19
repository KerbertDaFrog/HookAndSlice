using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOneAudioSource : MonoBehaviour
{
    [SerializeField]
    private AudioSource clip;

    public void PlayClip()
    {
        clip.Play();
    }

}
