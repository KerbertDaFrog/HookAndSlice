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
        targets.AddRange(harpoon.hitPoints);
        this.transform.SetParent(null);
        StartCoroutine(HookTravelPath());
    }

    private void Update()
    {
        Vector3 gun = harpoon.transform.position;
        Vector3 hook = this.transform.position;
        Vector3 a = targets[0];
        float distFromGun = Vector3.Distance(gun, hook);
        float distFromTarget = Vector3.Distance(hook, a);
        this.transform.position = Vector3.MoveTowards(hook, a, speed);
    }

    private IEnumerator HookTravelPath()
    {
        yield return new WaitForSeconds(0.1f);
    }
}
