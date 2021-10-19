using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager _instance;

    public static StatsManager Instance { get { return _instance; } }

    public int hooksShot;
    public int enemiesKilled;
    public int secretsFound;

    private void Awake()
    {
        if(_instance != null && _instance != this)
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
}
