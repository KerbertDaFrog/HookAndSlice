using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Hook : MonoBehaviour
{
    [SerializeField]
    private bool movingToPoint;

    [SerializeField]
    private float speed;

    [SerializeField]
    private int damage;

    public List<Vector3> targets = new List<Vector3>();

    public Harpoon harpoon;

    private Transform destination;

    private void Awake()
    {
        harpoon = FindObjectOfType<Harpoon>();
    }

    private void Start()
    {
        //StartCoroutine(HookTravelPath());

        //int targetPoints = targets.Count;

        //for (int i = 0; i < targetPoints; i++)
        //{

        //}
    }

    private void Update()
    {
        
    }

    private IEnumerator HookTravelPath()
    {
        targets.AddRange(harpoon.hitPoints);
        yield return new WaitForSeconds(0.1f);
        Vector3 a = transform.position;
        Vector3 b = targets[0];
        Vector3 c = targets[1];
        Vector3 d = targets[2];
        Vector3 e = targets[3];
        Vector3 f = targets[4];
        Vector3.MoveTowards(a, b, speed);
        //Checks to see if the first index of the Vector3 list is null. If not, it will assign a value index 0 with value b, then check if the next index is null and continue the process until it finds a null index or reaches the end.
        
    }
}
