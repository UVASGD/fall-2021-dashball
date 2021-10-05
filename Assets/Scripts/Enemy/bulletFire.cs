using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletFire : MonoBehaviour
{
public float bulletForce = 100.0f;
public Rigidbody2D rb;


void Start ()
{
    //fires bullet with it's force
    rb.velocity = transform.right*bulletForce;
}


void OnTriggerEnter2D(Collider2D target)
{   //display name of what is hit
    Debug.Log(target.name);
    //for somereason really hates camerabounds of unmoving camera
    if(target.name != "CameraBounds")
    //removes object
    Destroy(gameObject);
}


}
