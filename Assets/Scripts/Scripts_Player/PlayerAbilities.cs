using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerFootsteps playerFootsteps;
    private PlayerStats playerStats;

    private Transform viewRoot;
    private float heightStanding = 1.8f;
    private float heightCrouching = 1f;

    private float volumeSprint = 1f;
    private float volumeCrouch = 0.1f;
    private float volumeWalkMin = 0.2f, volumeWalkMax = 0.6f;

    private float walkDistanceFootsteps = 0.45f;
    private float sprintDistanceFootsteps = 0.3f;
    private float crouchDistanceFootsteps = 0.6f;

    private bool isCrouching;

    private float sprintValue = 100f;

    public float sprintThreshold = 10f;

    public float sprintSpeed = 10f;
    public float moveSpeed = 5f;
    public float crouchSpeed = 2f;


    void Start() {
        playerFootsteps.minVolume = volumeWalkMin;
        playerFootsteps.maxVolume = volumeWalkMax;
        playerFootsteps.distanceFootsteps = walkDistanceFootsteps;
    }

    void Awake() {
        playerMovement = GetComponent<PlayerMovement>();
        viewRoot = transform.GetChild(0); //get View Root
        playerFootsteps = GetComponentInChildren<PlayerFootsteps>();
        playerStats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        sprint();
        crouch();
    }

    //sprint function
    void sprint() {
        //sprint if we have stamina
        if(sprintValue > 0f) {
            if(Input.GetKeyDown(KeyCode.LeftShift) && !isCrouching) {
                playerMovement.speed = sprintSpeed;

                playerFootsteps.distanceFootsteps = sprintDistanceFootsteps;
                playerFootsteps.minVolume = volumeSprint;
                playerFootsteps.maxVolume = volumeSprint;
            }
        }

        if(Input.GetKeyUp(KeyCode.LeftShift) && !isCrouching) {
            playerMovement.speed = moveSpeed;

            playerFootsteps.distanceFootsteps = walkDistanceFootsteps;
            playerFootsteps.minVolume = volumeWalkMin;
            playerFootsteps.maxVolume = volumeWalkMax;
        }

        if(Input.GetKey(KeyCode.LeftShift) && !isCrouching) {
            sprintValue -= sprintThreshold * Time.deltaTime;
            if(sprintValue <= 0f) {
                sprintValue = 0f;

                //reset speed and sound
                playerMovement.speed = moveSpeed;
                playerFootsteps.distanceFootsteps = walkDistanceFootsteps;
                playerFootsteps.minVolume = volumeWalkMin;
                playerFootsteps.maxVolume = volumeWalkMax;
            }

            //call playerStats
            playerStats.displayStaminaStats(sprintValue);

        } else {
            if(sprintValue != 100f) {
                //generate stamina back
                sprintValue += (sprintThreshold / 2f) * Time.deltaTime;
                
                playerStats.displayStaminaStats(sprintValue);

                //stop from generating more than 100 stamina
                if(sprintValue > 100f) {
                    sprintValue = 100f;
                }
            }
        }
    }

    //crouch function
    void crouch() {
        if(Input.GetKeyDown(KeyCode.LeftControl)) {
            if(isCrouching) {
                viewRoot.localPosition = new Vector3(0f, heightStanding, 0f);
                playerMovement.speed = moveSpeed;

                playerFootsteps.distanceFootsteps = walkDistanceFootsteps;
                playerFootsteps.minVolume = volumeWalkMin;
                playerFootsteps.maxVolume = volumeWalkMax;

                isCrouching = false;
            } else {
                viewRoot.localPosition = new Vector3(0f, heightCrouching, 0f);
                playerMovement.speed = crouchSpeed;

                playerFootsteps.distanceFootsteps = crouchDistanceFootsteps;
                playerFootsteps.minVolume = volumeCrouch;
                playerFootsteps.maxVolume = volumeCrouch;

                isCrouching = true;
            }
        }
    }




















}
