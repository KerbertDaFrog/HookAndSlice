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

    private int time = 0;
    private int enemies = 0;
    private int hooks = 0;

    private int endhooks = 5;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Display();
        }
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
        enemiesKilledText.text = "" + enemies;
        hooksShotText.text = "" + hooks;
    }





}
