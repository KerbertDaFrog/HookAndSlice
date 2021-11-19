using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomON : MonoBehaviour
{

    [SerializeField]
    private GameObject prisonWall;

    [SerializeField]
    private GameObject bossRoom;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            prisonWall.SetActive(true);
            bossRoom.SetActive(true);
            Destroy(gameObject);
        }
    }

}
