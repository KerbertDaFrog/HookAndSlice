using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{

    private LineRenderer lr;

    [SerializeField]
    private Transform[] points;

    private Harpoon harpoon;
    private List<Vector3> bouncepoints = new List<Vector3>();

    //private GameObject firePoint;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        harpoon = FindObjectOfType<Harpoon>();
        SetUpLine(points);
        //firePoint = GameObject.Find("FirePoint");
        //Vector3 firePos = firePoint.transform.position;
    }

    private void Start()
    {
        bouncepoints.AddRange(harpoon.hitPoints);
    }

    private void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            lr.SetPosition(i, points[i].position);
        }
    }

    public void SetUpLine(Transform[] points)
    {
        lr.positionCount = points.Length;
        this.points = points;
    }
}
