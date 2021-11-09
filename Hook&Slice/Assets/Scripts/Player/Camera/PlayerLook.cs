using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public static PlayerLook instance;

    [SerializeField]
    private float sensX;
    [SerializeField]
    private float sensY;

    [SerializeField]
    Transform cam;
    [SerializeField]
    Transform orientation;

    private float mouseX;
    private float mouseY;

    //private float multi = 0.01f;

    private float xRot;
    private float yRot;

    private float sesitivity;

    //if paused the player can't look around
    public PauseMenu pm;

    private bool ispaused;


    private void Awake()
    {
        instance = this;
        pm = FindObjectOfType<PauseMenu>();
        sesitivity = SettingsManager.Instance.mouseSensitivity;
    }


    private void OnEnable()
    {
        pm.isGamePaused += GamePause;
    }

    private void OnDisable()
    {
        pm.isGamePaused -= GamePause;
    }

    // Start is called before the first frame update
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!ispaused)
        {
            PlayerInput();
        }
        

        cam.transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        orientation.transform.rotation = Quaternion.Euler(0, yRot, 0);
    }

    private void PlayerInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRot += mouseX * sesitivity;
        xRot -= mouseY * sesitivity;

        xRot = Mathf.Clamp(xRot, -90f, 90f);
    }

    //gets called from PauseMenu
    private void GamePause(bool paused)
    {
        ispaused = paused;
        sesitivity = SettingsManager.Instance.mouseSensitivity;
    }


    //this gets called from SettingsManager Script
    public void SetPlayerSensitivity()
    {
        //sesitivity = SettingsManager.Instance.mouseSensitivity;
    }

}
