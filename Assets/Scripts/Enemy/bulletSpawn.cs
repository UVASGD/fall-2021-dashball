using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletSpawn : MonoBehaviour
{
 public GameObject bullet;
 public Transform spawnPoint;

 void Update ()
 {
    //check to see if shoot
    //getbutton down used for testing (right click)
     if (Input.GetButtonDown("Fire2")) 
     Shoot();
     
  
 }

    void Shoot()
    {
        //fire bullet from fire point
    Instantiate(bullet,spawnPoint.position, spawnPoint.rotation);
    }

}
