using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Harpoon2 : MonoBehaviour
{
    public int maxReflectionCount = 5;
    public float maxStepDistance = 200;

    private void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Handles.ArrowHandleCap(0, this.transform.position + this.transform.forward * 0.25f, this.transform.rotation, 0.5f, EventType.Repaint);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, 0.25f);

        DrawPredictionReflectionPattern(this.transform.position + this.transform.forward * 0.75f, this.transform.forward, maxReflectionCount);
    }

    void DrawPredictionReflectionPattern(Vector3 position, Vector3 direction, int reflectionsRemaining)
    {
        if (reflectionsRemaining == 0)
        {
            return;
        }

        Vector3 startingPosition = position;

        Ray ray = new Ray(position, direction);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxStepDistance))
        {
            direction = Vector3.Reflect(direction, hit.normal);
            position = hit.point;
        }
        else
        {
            position += direction * maxStepDistance;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(startingPosition, position);

        DrawPredictionReflectionPattern(position, direction, reflectionsRemaining - 1);

        //[SerializeField]
        //private int rayCount = 2;

        //private LineRenderer lr;

        //// Start is called before the first frame update
        //void Start()
        //{
        //    lr = GetComponent<LineRenderer>();
        //    lr.material = new Material(Shader.Find("Sprites/Default"));
        //}

        //// Update is called once per frame
        //void Update()
        //{
        //    CastRay(transform.position, transform.forward);

        //    lr.positionCount = rayCount;
        //}

        //private void CastRay(Vector3 position, Vector3 direction)
        //{
        //    for(int i = 0; i < rayCount; i++)
        //    {
        //        lr.SetPosition(i, Vector3.zero);
        //    }

        //    for(int i = 0; i < rayCount; i++)
        //    {
        //        Ray ray = new Ray(position, direction);
        //        RaycastHit hit;

        //        if(Physics.Raycast(ray, out hit, 10, 1))
        //        {
        //            lr.SetPosition(i, hit.point);
        //            Debug.DrawLine(position, hit.point, Color.red);
        //            position = hit.point;
        //            direction = Vector3.Reflect(direction, hit.normal);
        //        }
        //        else
        //        {
        //            lr.SetPosition(i, direction * 10f);
        //            Debug.DrawRay(position, direction * 10, Color.blue);
        //            break;
        //        }
        //    }
        //}
    }
}
