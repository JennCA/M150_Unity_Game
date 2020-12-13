using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 moveDirection; //calculate what is the direction we are going to

    public float speed = 5f;
    public float jumpForce = 10f;
    private float gravity = 20f;
    private float verticalVelocity;

    void Awake() {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement();
    }

    //move player
    void playerMovement() {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection); //from local to world space
        moveDirection *= speed * Time.deltaTime;

        applyGravity();

        characterController.Move(moveDirection);
    }

    void applyGravity() {
        verticalVelocity -= gravity * Time.deltaTime;

        //player jumps
        jumpMovement();

        moveDirection.y = verticalVelocity * Time.deltaTime;
    }

    void jumpMovement() {
        if(characterController.isGrounded && Input.GetKeyDown(KeyCode.Space)) {
            verticalVelocity = jumpForce;
        }
    }
}
