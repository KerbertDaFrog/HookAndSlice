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

    public int index = 0;

    public float t;

    public Harpoon harpoon;

    private Transform target1;
    private Transform target2;

    private void Awake()
    {
        harpoon = FindObjectOfType<Harpoon>();
    }

    private void Start()
    {    
        StartCoroutine(HookTravelPath());
    }

    private void Update()
    {
        Vector3 a = target1.position;
        Vector3 b = target2.position;
        transform.position = Vector3.MoveTowards(a, Vector3.Lerp(a, b, t), speed);
    }

    private IEnumerator HookTravelPath()
    {
        targets.AddRange(harpoon.hitPoints);
        yield return new WaitForSeconds(0.1f);
        foreach (Vector3 target in targets)
        {
            index++;
            print(targets.IndexOf(target));
        }

        Vector3 a = transform.position;
        Vector3 b = targets[0];
        yield return new WaitForSeconds(0.1f);
        Vector3.MoveTowards(a, b, speed);
        for (int i = 0; i < targets.Count; i++)
        {

        }
    }
}
