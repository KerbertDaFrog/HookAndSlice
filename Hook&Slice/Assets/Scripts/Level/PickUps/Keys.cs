﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : Pickup
{
    [SerializeField]
    private GameObject icon;

    [Header("Doors to Unlock")]

    [SerializeField]
    private DoorOpen door;

    public override void Result()
    {
        door.UnLock();
        icon.SetActive(true);
    }
}
