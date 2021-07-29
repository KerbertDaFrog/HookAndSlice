using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Hook : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private int damage;

    [SerializeField]
    private List<Vector3> targets = new List<Vector3>();

    public Harpoon harpoon;

    [SerializeField]
    private Vector3 origin;
    [SerializeField]
    private Vector3 target;

    [SerializeField]
    private float dist;

    private int index = 0;

    private void Awake()
    {
        harpoon = FindObjectOfType<Harpoon>();
    }

    private void Start()
    {       
        targets.AddRange(harpoon.hitPoints);
        this.transform.SetParent(null);
    }

    private void Update()
    {
        print("my target = " + index);

        //origin = harpoon.originPoint

        dist = Vector3.Distance(origin, target);
        if (dist < 0.1)
        {
            index++;
        }      

        if (index == targets.Count - 1 && dist < 0.1)
        {
            Destroy(gameObject);
        }

        origin = transform.position;

        if(targets != null)
        {
            target = targets[index];
        }
        
        transform.position = Vector3.MoveTowards(origin, target, speed);
    }
}
