using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDamage : MonoBehaviour
{
    private SpriteRenderer mat;

    private void OnEnable()
    {
        mat = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            TakeDamage();
        }
    }

    IEnumerator TakeDamageColor()
    {
        yield return null;
    }

    private void TakeDamage()
    {
        mat.material.SetColor("_EmissionColor", Color.grey);
    }
}
