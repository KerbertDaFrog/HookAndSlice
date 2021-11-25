using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class LevelWinDoor : MonoBehaviour
{
    [SerializeField]
    private KnightBoss kb;

    [SerializeField]
    protected Animator anim;

    [Header("Analytics:")]
    [SerializeField]
    protected int roomNumber;

    private void Update()
    {
        if(kb.currentState == Enemy.EnemyStates.dead)
        {
            anim.SetBool("doorOpen", true);
            //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Dungeon", "room: " + roomNumber);
            //HudControl.Instance.ProgressByRoom(roomNumber);
        }
    }
}
