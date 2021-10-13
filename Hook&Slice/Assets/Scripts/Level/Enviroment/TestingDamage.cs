using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingDamage : MonoBehaviour
{
    [SerializeField]
    private GameObject damagebox;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(OnOff());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator OnOff()
    {
        yield return new WaitForSeconds(0.5f);
        damagebox.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        damagebox.SetActive(false);
        StartCoroutine(OnOff());
    }


}
