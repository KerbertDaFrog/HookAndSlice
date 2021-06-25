using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpoon : MonoBehaviour
{
    [Header("Harpoon Modifiers")]
    private float damage;
    private float speed;
    private float range;

    private bool fired = false;

    //Vector Variables
    private Vector3 origin;
    private Vector3 endPoint;
    private Vector3 mousePos;
    private Vector3 rayHitPos;
    private float rayHitDistance;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject point;

    private LineRenderer line;

    // Start is called before the first frame update
    private void Start()
    {
        line = GetComponent<LineRenderer>();
        line.enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            FireHarpoon();
        }

        if(Input.GetMouseButtonUp(0))
        {
            fired = false;
        }

        HarpCol();

        origin = player.transform.position + player.transform.forward * 0.5f * player.transform.lossyScale.z;

        line.SetPosition(0, origin);

        line.SetPosition(1, endPoint);
    }

    private void FireHarpoon()
    {
        if(fired != true)
        {
            line.enabled = true;
            fired = true;
        }

        if (rayHitDistance < range)
        {
            endPoint = rayHitPos;
        }
        else
        {
            endPoint = origin + player.transform.forward * range;
        }
    }

    private void HarpCol()
    {
        var ray = new Ray(transform.position, transform.forward);
        var beamPoint = (transform.position + Vector3.up);
        Vector3 dir = endPoint - origin;
        dir.Normalize();

        RaycastHit hit;

        if (Physics.Raycast(origin, dir, out hit, range))
        {
            ////Damage Enemy Health
            //if (hit.collider.gameObject.tag == "Enemy")
            //{
            //    if (hit.collider.gameObject.GetComponent<EnemyHealth>())
            //    {
            //        //endPoint = hit.point;
            //        hit.collider.gameObject.GetComponent<EnemyHealth>().HurtEnemy(damageToGive);
            //        Debug.Log(hit.transform.name);
            //    }
            //}

            if (hit.collider)
            {
                //endPoint = hit.point;
                Debug.Log(hit.collider.gameObject.name);
            }

            rayHitDistance = hit.distance;
            rayHitPos = hit.point;
        }
    }
}
