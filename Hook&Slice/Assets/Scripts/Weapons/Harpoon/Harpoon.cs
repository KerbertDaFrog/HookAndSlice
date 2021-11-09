﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UI;

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

    [SerializeField]
    private Slider cooldownSlider;

    private Camera fpsCam;

    [SerializeField]
    private LayerMask enemy;

    public delegate void HarpoonCooldown(float remaining, float max);
    public HarpoonCooldown harpoonCooldown;

    public PauseMenu pm;

    private bool ispaused;

    private void Start()
    {        
        fpsCam = Camera.main;
        harpoonCooldown(currentCDTimer, setCDTimer);
    }

    private void Awake()
    {
        pm = FindObjectOfType<PauseMenu>();
    }

    private void OnEnable()
    {
        pm.isGamePaused += GamePause;
    }

    private void OnDisable()
    {
        pm.isGamePaused -= GamePause;
    }

    private void Update()
    {
        if (!ispaused)
        {
            transform.rotation = fpsCam.transform.rotation;

            if (Input.GetMouseButtonDown(0))
            {
                PredictionReflectionPattern(this.transform.position + this.transform.forward * 0.05f, this.transform.forward, maxReflectionCount);

                if (!hasShot && hitPoints.Count != 0 && currentCDTimer <= 0)
                {
                    hasShot = true;
                    staticHook.SetActive(false);
                    activeHook = Instantiate(hook, firePoint).gameObject;
                    AudioManager.instance.Play("ChainMovement");
                    AudioManager.instance.Play("HarpoonShoot");
                }

                if (!hasShot)
                    hitPoints.Clear();
            }

            if (Input.GetKeyDown(KeyCode.LeftControl) && currentCDTimer <= 0 && hasShot)
            {
                hookCancelled = true;
                AudioManager.instance.StopPlaying("ChainMovement");
                AudioManager.instance.Play("HarpoonReload");
            }              

            if (returned)
            {
                currentCDTimer = setCDTimer;
                harpoonCooldown(currentCDTimer, setCDTimer);
                AudioManager.instance.StopPlaying("ChainMovement");
                AudioManager.instance.Play("HarpoonReload");
                returned = false;
            }

            if (!hasShot)
            {
                currentCDTimer = Mathf.Clamp(currentCDTimer -= Time.deltaTime, 0f, setCDTimer);
                harpoonCooldown(currentCDTimer, setCDTimer);
            }

            if (hookCancelled)
            {
                hasShot = false;
                currentCDTimer = setCDTimer;
                if (activeHook == null)
                {
                    hookCancelled = false;
                }
            }

            if (activeHook == null)
                hasShot = false;
        }  
    }

    private void GamePause(bool paused)
    {
        ispaused = paused;
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

        DrawPredictionReflectionPattern(this.transform.position + this.transform.forward * 0.05f, this.transform.forward, maxReflectionCount);
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
        if (Physics.Raycast(ray, out hit, maxStepDistance, ~enemy))
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
        if (Physics.Raycast(ray, out hit, maxStepDistance, ~enemy))
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