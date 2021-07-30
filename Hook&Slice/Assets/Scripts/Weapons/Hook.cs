using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Hook : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float dist;
    [SerializeField]
    private float distanceFromGun;

    [SerializeField]
    private bool done;

    [SerializeField]
    private int damage;

    private int index = 0;

    [SerializeField]
    private List<Vector3> targets = new List<Vector3>();

    private Harpoon harpoon;

    [SerializeField]
    private Vector3 origin;
    [SerializeField]
    private Vector3 target; 

    private void Awake()
    {
        harpoon = FindObjectOfType<Harpoon>();
    }

    private void Start()
    {
        speed = 0.3f;
        //Take all of the elements from the hitPoints list in the Harpoon Component and add/copy them to the targets list in this script.
        targets.AddRange(harpoon.hitPoints);
        this.transform.SetParent(null);
    }

    private void Update()
    {
        //If the targets list is not null, then the Vector3 variable for target will be equal to the first element of targets index.
        if (targets != null && !done)
        {
            target = targets[index];
        }

        origin = transform.position;

        if(!done)
            print("my target = " + index);

        //origin = harpoon.originPoint

        dist = Vector3.Distance(origin, target);

        distanceFromGun = Vector3.Distance(this.transform.position, harpoon.transform.position);

        //If the distance to the target is less 0.1 than change the index to one element up.
        if (dist < 0.1)
        {
            index++;
        }      

        //If the index int is equal to the amount of elements in the Vector3 targets list and the distance to the target is less than 0.1, do this.
        if (index == targets.Count && dist < 0.1)
        {
            done = true;
        }

        //If done, change the target to the harpoon gun's transform position.
        if(done)
        {
            target = harpoon.transform.position;
        }

        //If done and the hook's distance to the harpoon gun is less than 0.1, turn off done, turn off hasShot to false in Harpoon script, and destroy hook game object.
        if(done && distanceFromGun < 0.1)
        {
            done = false;
            harpoon.hasShot = false;
            Destroy(gameObject);
        }

        transform.LookAt(target);
       
        transform.position = Vector3.MoveTowards(origin, target, speed);
    }
}
