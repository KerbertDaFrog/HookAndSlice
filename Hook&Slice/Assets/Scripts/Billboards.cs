using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboards : MonoBehaviour
{
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.flipX = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(PlayerLook.instance.transform.position, -Vector3.forward);
    }
}
