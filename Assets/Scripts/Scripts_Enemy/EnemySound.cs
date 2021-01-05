using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip screamClip, deathClip;

    [SerializeField]
    private AudioClip[] attackClips;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playScreamSound() {
        audioSource.clip = screamClip;
        audioSource.Play();
    }

    public void playDeathSound() {
        audioSource.clip = deathClip;
        audioSource.Play();
    }

    public void playAttackSound() {
        //pick random clip
        audioSource.clip = attackClips[Random.Range(0, attackClips.Length)];
        audioSource.Play();
    }
}
