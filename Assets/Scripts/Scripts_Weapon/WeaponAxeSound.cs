using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAxeSound : MonoBehaviour
{
    [SerializeField] //shows private variable in the editor
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip[] slashSounds;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void playSlashSound() {
        //switch between the 3 axe-sounds
        audioSource.clip = slashSounds[Random.Range(0, slashSounds.Length)];
        audioSource.Play();
    }
}
