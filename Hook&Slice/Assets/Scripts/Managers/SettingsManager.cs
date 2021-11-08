using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager _instance;

    public static SettingsManager Instance { get { return _instance; } }

    public float mouseSensitivity = 1;
    public bool safeLights;

    public AudioMixer audioMixer;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void OnApplicationQuit()
    {
        _instance = null;
    }


    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volumeParamaters", volume);
    }

}
