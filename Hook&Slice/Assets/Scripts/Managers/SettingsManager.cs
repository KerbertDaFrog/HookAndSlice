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
    [SerializeField]
    private Slider volumeSlider;
    private float audiovolume = 1f;


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
        volumeSlider.value = audiovolume;
    }

    private void OnApplicationQuit()
    {
        _instance = null;
    }

    public void SetVolume(float audiosliderValue)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(audiosliderValue) * 20);
        audiovolume = audiosliderValue;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

}
