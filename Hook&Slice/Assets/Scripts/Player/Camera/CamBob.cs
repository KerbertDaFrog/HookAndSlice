using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamBob : MonoBehaviour
{
    [Header("Transform References")]
    [SerializeField]
    private Transform headTransform;
    [SerializeField]
    private Transform camTransform;

    [Header("Head Bob")]
    [SerializeField]
    private float bobFrequency = 5f; 
    [SerializeField]
    private float bobHorAmp = 0.1f;
    [SerializeField]
    private float bobVerAmp = 0.2f;
    [SerializeField]
    [Range(0, 1)] private float headBobSmooth = 0.1f;

    //State
    [SerializeField]
    private bool isWalking;
    private float walkingTime;
    private Vector3 targetCamPos;

    private void Update()
    {
        //Checks to see if player is moving
        //Remove GetComponent in future
        if (GetComponent<PlayerMovement>().isMoving != false)
            isWalking = true;
        else
            isWalking = false;

        //Checks to see if player is sprinting
        //Remove GetComponent in future 
        if (GetComponent<PlayerMovement>().isMoving != false && GetComponent<PlayerMovement>().isSprinting != false)
            bobFrequency = 6f;
        else
            bobFrequency = 5f;

        //Set time and offset to 0
        if (!isWalking) walkingTime = 0;
        else walkingTime += Time.deltaTime;

        //Calculate the camera's target position
        targetCamPos = headTransform.position + CalculateHeadBobOffset(walkingTime);

        //Interpolate position
        camTransform.position = Vector3.Lerp(camTransform.position, targetCamPos, headBobSmooth);
        //Snap to position if it is close enough
        if ((camTransform.position - targetCamPos).magnitude <= 0.001) 
            camTransform.position = targetCamPos;
    }

    private Vector3 CalculateHeadBobOffset(float t)
    {
        float horOffset = 0;
        float verOffset = 0;
        Vector3 offset = Vector3.zero;

        if(t > 0)
        {
            //Calculate offsets
            horOffset = Mathf.Cos(t * bobFrequency) * bobHorAmp;
            verOffset = Mathf.Sin(t * bobFrequency * 2) * bobVerAmp;

            //Combine offsets relative to the head's position and calculate the camera's target position
            offset = headTransform.right * horOffset + headTransform.up * verOffset;
        }

        return offset;
    }
}
