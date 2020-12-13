using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    private PlayerMovement playerMovement;

    private Transform viewRoot;
    private float heightStanding = 1.8f;
    private float heightCrouching = 1f;

    private bool isCrouching;

    public float sprintSpeed = 10f;
    public float moveSpeed = 5f;
    public float crouchSpeed = 2f;



    void Awake() {
        playerMovement = GetComponent<PlayerMovement>();
        viewRoot = transform.GetChild(0); //get View Root
    }

    // Update is called once per frame
    void Update()
    {
        sprint();
        crouch();
    }

    //sprint function
    void sprint() {
        if(Input.GetKeyDown(KeyCode.LeftShift) && !isCrouching) {
            playerMovement.speed = sprintSpeed;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift) && !isCrouching) {
            playerMovement.speed = moveSpeed;
        }
    }

    //crouch function
    void crouch() {
        if(Input.GetKeyDown(KeyCode.LeftControl)) {
            if(isCrouching) {
                viewRoot.localPosition = new Vector3(0f, heightStanding, 0f);
                playerMovement.speed = moveSpeed;
                isCrouching = false;
            } else {
                viewRoot.localPosition = new Vector3(0f, heightCrouching, 0f);
                playerMovement.speed = crouchSpeed;
                isCrouching = true;
            }
        }
    }




















}
