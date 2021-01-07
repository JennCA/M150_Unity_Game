using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    [SerializeField]
    private Transform playerRoot, viewRoot;

    [SerializeField]
    private bool invert;

    [SerializeField]
    private bool canUnlock = true;

    [SerializeField]
    private float sensivity = 5f;

    [SerializeField]
    private float rollAngle = 10f;

    [SerializeField]
    private float rollSpeed = 3f;

    [SerializeField]
    private int smoothSteps = 10;

    [SerializeField]
    private float smoothWeight = 0.4f;

    private float currentRollAngle;
    private int lastViewFrame;

    [SerializeField]
    private Vector2 viewLimits = new Vector2(-70f, 80f);
    private Vector2 viewAngles;
    private Vector2 currentMouseView;
    private Vector2 smoothMove;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //lock cursor
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        cursorLockUnlock();

        if(Cursor.lockState == CursorLockMode.Locked) {
            lookAround();
        }
    }

    //lock and unlock the cursor
    void cursorLockUnlock() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(Cursor.lockState == CursorLockMode.Locked) {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            } else {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    //look around
    void lookAround() {
        currentMouseView = new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X")); //detecting movement of the mouse

        viewAngles.x += currentMouseView.x * sensivity * (invert ? 1f : -1f); //if invert "true" use 1f
        viewAngles.y += currentMouseView.y * sensivity;

        //will not allow vA.x to go below vL.x and above vL.y
        viewAngles.x = Mathf.Clamp(viewAngles.x, viewLimits.x, viewLimits.y);

        viewRoot.localRotation = Quaternion.Euler(viewAngles.x, 0f, 0f);
        playerRoot.localRotation = Quaternion.Euler(0f, viewAngles.y, 0f);
    }
}
