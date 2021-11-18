using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOneSound : MonoBehaviour
{

    [SerializeField]
    private string audioClipName;

    private void PlaySound()
    {
        AudioManager.instance.Play(audioClipName);
    }

}
