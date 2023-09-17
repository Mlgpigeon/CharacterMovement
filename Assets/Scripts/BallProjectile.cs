using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallProjectile : MonoBehaviour
{   
    public Rigidbody rigidbody;
    
    //Script that describes the physics of a ball thrown by the player
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        float speed = 25f;
        
        rigidbody.velocity = transform.forward * speed;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
