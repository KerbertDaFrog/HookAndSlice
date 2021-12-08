using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    [SerializeField]
    private Animator door;

    [SerializeField]
    private GameObject normalLevel;

    [SerializeField]
    private GameObject prisonWall;

    private void OnTriggerEnter(Collider other)
    {
      if(other.gameObject.tag == "Player")
      {
            TriggerBoss();
      }
    }

    private void TriggerBoss()
    {
        AudioManager.instance.StopPlaying("DungeonMusic");
        AudioManager.instance.Play("KnightMusic");
        door.SetBool("doorClose", true);
        door.SetBool("doorOpen", false);
        prisonWall.SetActive(false);
        SettingsManager.Instance.bossRoomReached = true;
        HudControl.Instance.bossRoom = true;
        Destroy(gameObject);
    }

}
