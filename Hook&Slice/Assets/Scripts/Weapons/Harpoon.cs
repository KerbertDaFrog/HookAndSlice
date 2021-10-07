using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Harpoon : MonoBehaviour
{
    [Header("Transform Variables")]
    public GameObject staticHook;
    private GameObject activeHook;
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private Transform hook;

    [Header("Hook Distance & Reflection Amount")]
    private int maxReflectionCount = 5;
    private float maxStepDistance = 200;

    [Header("Vector3 Hit Points")]
    public List<Vector3> hitPoints = new List<Vector3>();

    [Header("Bools")]
    public bool hasShot;
    public bool hookCancelled;
    public bool returned;
    
    [Header("Cooldown Timer")]
    [SerializeField]
    private float currentCDTimer;
    private float setCDTimer = 1f;

    private Camera fpsCam;    

    private void Start()
    {        
        fpsCam = Camera.main;
    }

    private void Update()
    {
        transform.rotation = fpsCam.transform.rotation;

        if (Input.GetMouseButtonDown(0))
        {
            PredictionReflectionPattern(this.transform.position + this.transform.forward * 0.75f, this.transform.forward, maxReflectionCount);         

            if (!hasShot && hitPoints.Count != 0 && currentCDTimer <= 0)
            {
                hasShot = true;
                staticHook.SetActive(false);
                activeHook = Instantiate(hook, firePoint).gameObject;
            }

            if (!hasShot)
                hitPoints.Clear();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && currentCDTimer <= 0 && hasShot)
            hookCancelled = true;

        if (returned)
        {
            currentCDTimer = setCDTimer;
            returned = false;
        }

        if(!hasShot)
            currentCDTimer = Mathf.Clamp(currentCDTimer -= Time.deltaTime, 0f, setCDTimer);

        if(hookCancelled)
        {
            hasShot = false;
            currentCDTimer = setCDTimer;
            if(activeHook == null)
            {
                hookCancelled = false;
            }
        }

        if (activeHook == null)
            hasShot = false;
    }

    #region ReflectionPattern
    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        Handles.color = Color.red;
        Handles.ArrowHandleCap(0, this.transform.position + this.transform.forward * 0.25f, this.transform.rotation, 0.5f, EventType.Repaint);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, 0.25f);
#endif 

        DrawPredictionReflectionPattern(this.transform.position + this.transform.forward * 0.75f, this.transform.forward, maxReflectionCount);
    }

    void DrawPredictionReflectionPattern(Vector3 position, Vector3 direction, int reflectionsRemaining)
    {
        if (reflectionsRemaining == 0)
        {
            //fire projectile
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

#if UNITY_EDITOR
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(startingPosition, position);
#endif

        DrawPredictionReflectionPattern(position, direction, reflectionsRemaining - 1);
    }

    void PredictionReflectionPattern(Vector3 position, Vector3 direction, int reflectionsRemaining)
    {
        if (reflectionsRemaining == 0)
        {
            //fire projectile
            return;
        }

        if (reflectionsRemaining == maxReflectionCount)
            hitPoints.Clear();

        Ray ray = new Ray(position, direction);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxStepDistance))
        {
            direction = Vector3.Reflect(direction, hit.normal);
            hitPoints.Add(hit.point);
            if (hitPoints.Count > maxReflectionCount)
                hitPoints.RemoveAt(0);
            position = hit.point;
        }
        else
        {
            position += direction * maxStepDistance;
        }

        PredictionReflectionPattern(position, direction, reflectionsRemaining - 1);
    }
    #endregion
}