using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpoon : MonoBehaviour
{
    [Header("Harpoon Modifiers")]
    private float damage;
    private float speed;
    private float length;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            FireHarpoon();
        }
    }

    private void FireHarpoon()
    {

    }
}
