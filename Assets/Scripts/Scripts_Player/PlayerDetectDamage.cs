using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectDamage : MonoBehaviour
{
    public LayerMask layerMask;

    public float damage = 2f;
    public float radius = 1f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, layerMask);

        if(hits.Length > 0) {
            hits[0].gameObject.GetComponent<HealthScript>().applyDamage(damage);
            gameObject.SetActive(false);
        }
    }
}
