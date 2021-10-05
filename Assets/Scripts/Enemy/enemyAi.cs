using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAi : Destructible
{
    //controls speed at which enemy moves in x plain
    public float xSpeed = 2.0f;
    //controls speed at which enemy moves in x plain
    public float ySpeed = 2.0f;

    //controls speed at which enemies move when they have no preference for plain
    public float speed = 1.0f;
    //distance enemy tries to maintain away from player
    public float distance = 1.0f;
    //set to player in the editor to target them

    //changed this from transform to gameObject to do damage stuff too
    //if you do target.transform you can do the same things
    public GameObject target;

    //attack-y stuff
    public float damage; //damage per attack
    public float swingTimer; //ie how long between swings
    public float lastSwing = 0; //the actual timer maybe I should use different names lol

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 50;
        hitPoints = 50;
    }

    // Update is called once per frame
    void Update()
    {
        //simple move test
        //transform.position = new Vector3 (transform.position.x + xSpeed, transform.position.y + ySpeed);
        
               //simple move towrds player if too far
        if (Vector3.Distance(transform.position,target.transform.position)>distance){
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);  
        }
        //attacks the player if close enough that its not moving towards it
        else{
            Attack(target.GetComponent<Destructible>());
        }

        //increments the swing timer
        lastSwing += Time.deltaTime;
    }

    private void Attack(Destructible target){
        if (lastSwing >= swingTimer){
            target.TakeDamage(damage);
            lastSwing = 0;
        }
    }

}
