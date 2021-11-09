using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetQuality : MonoBehaviour
{
    private Dropdown dropdown;
    public int graphicsetting;

    private void Awake()
    {
        dropdown = GetComponent<Dropdown>();
    }

    private void OnEnable()
    {
        graphicsetting = SettingsManager.Instance.graphics;
        dropdown.value = graphicsetting;
    }

    public void ChangeQuality(int quality)
    {
        graphicsetting = quality;
        SettingsManager.Instance.SetQuality(graphicsetting);
    }
}
