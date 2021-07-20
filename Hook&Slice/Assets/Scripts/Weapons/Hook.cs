using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    [SerializeField]
    private LayerMask colMask;

    private Rigidbody rb;

    [SerializeField]
    private float speed;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        HookTrajectory();
    }

    private void FixedUpdate()
    {       
        rb.AddForce(transform.forward * speed);
    }

    private void HookTrajectory()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        Ray ray = new Ray(origin, direction);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            direction = Vector3.Reflect(direction, hit.normal);
        }
    }
}
