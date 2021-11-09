using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager _instance;

    public static SettingsManager Instance { get { return _instance; } }



    public float mouseSensitivity = 1;


    public bool safeLights;

    
    
    public AudioMixer audioMixer;

    public float audioVolume = 1f;

    public int graphics = 4;

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

    private void OnEnable()
    {

    }

    private void OnApplicationQuit()
    {
        _instance = null;
    }

    public void SetVolume(float vol)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(vol) * 20);
        audioVolume = vol;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        graphics = qualityIndex;
    }

}
