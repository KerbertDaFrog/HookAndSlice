using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetLightsSafe : MonoBehaviour
{
    private Toggle toggle;
    public bool lights;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
    }

    private void OnEnable()
    {
        //lights = StatsManager.Instance.safeLights;
    }

    public void SetToggle(bool _lights)
    {
        lights = _lights;
        StatsManager.Instance.safeLights = lights;
    }
}
