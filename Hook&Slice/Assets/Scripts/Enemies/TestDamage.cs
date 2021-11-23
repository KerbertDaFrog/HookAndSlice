using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDamage : MonoBehaviour
{
    [Header("Damage Feedback")]
    [SerializeField]
    private SpriteRenderer mat;
    [SerializeField]
    private Material originalmat;
    [SerializeField]
    private Material damagemat;


    private void OnEnable()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            //TakeDamage();

            StartCoroutine("TakeDamageColor");
        }
    }

    IEnumerator DamageFeedback()
    {
        mat.material = damagemat;
        yield return new WaitForSeconds(0.1f);
        mat.material = originalmat;
        yield return null;
    }

    private void TakeDamage()
    {
        mat.material.SetColor("_EmissionColor", Color.grey);
    }

    private void TakeDamageV2()
    {
        mat.material = damagemat;
    }
}
