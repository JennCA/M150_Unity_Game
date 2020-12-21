using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    private AudioSource soundFootsteps;
    private CharacterController characterController;

    private float accumulatedDistance;

    [SerializeField]
    private AudioClip[] clipFootsteps;

    [HideInInspector]
    public float minVolume, maxVolume;

    [HideInInspector]
    public float distanceFootsteps;


    void Awake() {
        soundFootsteps = GetComponent<AudioSource>();
        characterController = GetComponentInParent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        footstepSoundCheck();
    }

    void footstepSoundCheck() {
        if(!characterController.isGrounded) {
            return;
        }

        if(characterController.velocity.sqrMagnitude > 0) {
            accumulatedDistance += Time.deltaTime; //calculate the distance we passed
            if(accumulatedDistance > distanceFootsteps) {
                soundFootsteps.volume = Random.Range(minVolume, maxVolume);
                soundFootsteps.clip = clipFootsteps[Random.Range(0, clipFootsteps.Length)];
                soundFootsteps.Play();

                accumulatedDistance = 0f;
            }
        } else {
            accumulatedDistance = 0f;
        }
    }
}
