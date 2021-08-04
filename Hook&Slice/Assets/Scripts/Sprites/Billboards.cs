using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboards : MonoBehaviour
{
    private Camera cam;

    private bool useStaticBillboard;

    // Start is called before the first frame update
    private void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if(!useStaticBillboard)
        {
            transform.LookAt(cam.transform);
            transform.Rotate(0, 180, 0);
        }
        else
        {
            transform.rotation = cam.transform.rotation;
        }

        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
    }
}
