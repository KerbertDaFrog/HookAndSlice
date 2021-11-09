using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SetVolume : MonoBehaviour
{

    private Slider slider;
    public float vol = 1;

    public delegate void Volume(float newvolume);
    public Volume volume;


    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void SetAudioVolume(float _vol)
    {
        vol = _vol;
        SettingsManager.Instance.SetVolume(vol);
    }

    private void OnEnable()
    {
        vol = SettingsManager.Instance.audioVolume;
        slider.value = vol;
    }

}
