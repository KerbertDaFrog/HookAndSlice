﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Harpoon2 : MonoBehaviour
{
    public int maxReflectionCount = 5;
    public float maxStepDistance = 200;

    public List<Vector3> hitPoints = new List<Vector3>();
    
    private bool hasShot;

    private Camera fpsCam;

    private void Start()
    {
        fpsCam = Camera.main;
    }

    private void Update()
    {
        transform.rotation = fpsCam.transform.rotation;

        if(Input.GetMouseButtonDown(0))
        {
            if(hasShot != true)
            {
                
            }

            PredictionReflectionPattern(this.transform.position + this.transform.forward * 0.75f, this.transform.forward, maxReflectionCount);
        }
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

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(startingPosition, position);

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

        Vector3 startingPosition = position;

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
}