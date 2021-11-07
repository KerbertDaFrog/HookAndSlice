using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomMusic : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
      if(other.gameObject.tag == "Player")
      {
        AudioManager.instance.StopPlaying("DungeonMusic");
        AudioManager.instance.Play("KnightMusic");
      }
    }
}
