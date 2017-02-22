using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryUnitOnCollision : MonoBehaviour
{
    public GameObject explosionEffect;
    public bool enableExplosion = false;

    // Use collisionInfo if I want to know which object collided, exact point of contact

    private void Start()
    {

    }

    private void OnTriggerEnter(/*Collider other*/)
    {
        Debug.Log("TEST");
        if(explosionEffect && enableExplosion) {
            Instantiate(explosionEffect, this.transform.position, Quaternion.identity);
            Destroy(this.transform.parent.gameObject);
            Debug.Log("TEST222");
        }
    }
}
