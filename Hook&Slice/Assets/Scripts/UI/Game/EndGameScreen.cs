using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameScreen : MonoBehaviour
{
    [SerializeField]
    private Text timeSpentText;

    [SerializeField]
    private Text enemiesKilledText;

    [SerializeField]
    private Text hooksShotText;

    private StatsManager stats;

    private int time;
    private int enemies ;
    private int hooks;

    private int endhooks = 5;

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    Display();
        //}
    }

    public void FinalStats()
    {
        
    }

    public int FinalHooks()
    {
        return hooks;
    }

    private void Display()
    {
        timeSpentText.text = "" + time;
        enemiesKilledText.text = "" + stats.enemiesKilled;
        hooksShotText.text = "" + stats.hooksShot;
    }
}
