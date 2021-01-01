using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Arrow_Spear : MonoBehaviour
{
    private Rigidbody arrowBody;

    public float speed = 30f;
    public float damage = 15f; //damage dealt to the enemy
    public float deactivateTimer = 3f; //deactivate the object after 3 seconds

    void Awake() {
        arrowBody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("deactivateGameObject", deactivateTimer); //call function "..." in deactivateTimer time
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void launch(Camera mainCamera) {
        //move towards the crosshair
        arrowBody.velocity = mainCamera.transform.forward * speed;
        transform.LookAt(transform.position + arrowBody.velocity);
    }

    void deactivateGameObject() {
        if(gameObject.activeInHierarchy) {
            gameObject.SetActive(false);
        }
    }

    //deactivate object after it touched/hit the enemy
    void onTriggerEnter(Collider target) {

    }
}
