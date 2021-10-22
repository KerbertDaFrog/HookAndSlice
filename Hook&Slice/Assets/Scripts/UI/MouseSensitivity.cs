using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetMouseSensitivity : MonoBehaviour
{
    private Slider slider;
    public float s = 1;

    public void SetSensitivity(float _s)
    {
        s = _s;
        StatsManager.Instance.mouseSensitivity = s;
    }
    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        s = StatsManager.Instance.mouseSensitivity;
        slider.value = s;
    }


}
