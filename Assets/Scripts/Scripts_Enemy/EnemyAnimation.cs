using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator animator;


    void Awake() {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void walk(bool walk) {
        animator.SetBool("Walk", walk);
    }

    public void run(bool run) {
        animator.SetBool("Run", run);
    }

    public void attack() {
        animator.SetTrigger("Attack");
    }

    public void death() {
        animator.SetTrigger("Death");
    }
}
