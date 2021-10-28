using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletSpawn : MonoBehaviour
{
 //what is fired
 public GameObject bullet;
//where bullet is spawned from
 public Transform spawnPoint;
//what to fire at
Transform target;

 //attack-y stuff (taken from spencers work on enemyAI)
    public float shootTimer; //ie how long between shots
    public float lastFire = 0; //the actual timer maybe I should use different names lol
    //offsetMin of bullet fire (0 by default)
    public float offsetMin =0.0f;
    //offsetMax of bullet fire (0 by default)
    public float offsetMax =0.0f;


   void Start()
    {
        //get components from objects
        target = GameObject.Find("Player").GetComponent<Transform>();
    }

 void Update ()
 {
    //check to see if shoot
    //incrememnt shot timer
    lastFire += Time.deltaTime;
    //fire if timer higher than shot clock
      if (lastFire >= shootTimer){
        lastFire = 0;
        Shoot();
      }
 }

    void Shoot()
    {
     //rotate spawnPoint to face player
    Vector2 direction = target.position - spawnPoint.position;
    direction.Normalize();
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //randomly add or subtract a number between max or min to fire angle
    bool addOrSub  = (Random.value > 0.5f);
    if(addOrSub)
    angle += Random.Range(offsetMin,offsetMax);
    else
    angle -= Random.Range(offsetMin,offsetMax);
    //rotate by angle
    spawnPoint.rotation = Quaternion.Euler(Vector3.forward * (angle));

        //fire bullet from spawnPoint
    Instantiate(bullet,spawnPoint.position, spawnPoint.rotation);
    }
}
