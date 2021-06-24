using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField]
    Transform camPos;

    private void Update()
    {
        transform.position = camPos.position;
    }
}
