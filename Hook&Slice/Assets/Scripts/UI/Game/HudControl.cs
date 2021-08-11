using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudControl : MonoBehaviour
{
    [Header("Collection")]
    [SerializeField]
    private GameObject collection;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CollectionON()
    {
        collection.SetActive(true);
        StartCoroutine(FeedbackOFF());
    }

    IEnumerator FeedbackOFF()
    {
        yield return new WaitForSeconds(1f);
        CollectionOFF();
    }


    //Collection Confirmation turn off?
    public void CollectionOFF()
    {
        collection.SetActive(false);
    }


}
