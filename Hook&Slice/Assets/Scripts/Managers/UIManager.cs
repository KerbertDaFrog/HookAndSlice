using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    private static UIManager _instance;

    [SerializeField]
    private GameObject deathScreen;

    [SerializeField]
    private GameObject winScreen;




    public static UIManager Instance { get { return _instance; } }

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






}
