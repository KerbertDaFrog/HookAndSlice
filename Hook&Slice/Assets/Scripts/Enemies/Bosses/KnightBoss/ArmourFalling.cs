using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourFalling : MonoBehaviour
{
    [SerializeField]
    private GameObject armourSprite;

    public void GroundHit()
    {
        //Audio Clip goes here
        armourSprite.SetActive(true);
        Destroy(gameObject);
    }
}
