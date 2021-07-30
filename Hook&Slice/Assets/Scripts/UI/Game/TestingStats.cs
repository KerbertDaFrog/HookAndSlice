using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestingStats : MonoBehaviour
{
    [SerializeField]
    private EndGameScreen endGS;


    private int hooks = 0;

    [SerializeField]
    private Text currentHooks;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.V))
        {
            endGS.FinalHooks();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            hooks += 1;
            NumbersTest();
        }


    }




    private void NumbersTest()
    {
        currentHooks.text = "Current Hooks:" + hooks;
    }



}
