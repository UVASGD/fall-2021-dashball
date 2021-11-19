using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAiStill : MonoBehaviour
{
   //what to chase after
    Transform target;
    //speed
    Rigidbody2D rb;
    //distance enemy will stop tracking player when they get this close (and melee range)
    public float stopChase = 2.0f;


    //melee enemy variables //from specners work in enemyAi.cs
      //attack-y stuff
    public float damage = 5.0f; //damage per attack
    public float swingTimer =1.0f; //ie how long between swings
    public float lastSwing = 0; //the actual timer maybe I should use different names lol

    void Start()
    {
        //get components from objects
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").GetComponent<Transform>();
        }

    void FixedUpdate()
    {   
        //increments the swing timer
        lastSwing += Time.deltaTime;

        //check distance from enemy to player to stop crowding
        float toTarget = Vector2.Distance(rb.position, target.position); 
       
        if(stopChase > toTarget)
        {
            //deal melee damage (add indicator)
            Attack(target.GetComponent<Destructible>());
        }
    }

    //taken from spencers work in enemyAi.cs
    private void Attack(Destructible target){
        if (lastSwing >= swingTimer){
            target.TakeDamage(damage);
            lastSwing = 0;
        }
    }


      public float giveScale()
    {
        //returns the distance  
        return stopChase;
    }
}