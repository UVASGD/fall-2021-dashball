using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class enemyAiPath : MonoBehaviour
{
    //what to chase after
    public Transform target;
    //speed
    public float speed = 200f;
    //how many nodes to look ahead
    public float nextWaypointDistance =3f;
    Path path;
    int currentWaypoint = 0;

    bool reachedEndofPath=false;
    Seeker seeker;
    Rigidbody2D rb;
    //distance enemy will stop tracking player when they get this close (and melee range)
    public float stopChase = 2.0f;


    //melee enemy variables //from specners work in enemyAi.cs
      //attack-y stuff
    public float damage; //damage per attack
    public float swingTimer; //ie how long between swings
    public float lastSwing = 0; //the actual timer maybe I should use different names lol

    void Start()
    {
        //get components from objects
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        //loop to find past
        InvokeRepeating("UpdatePath", 0f,.5f);
 
    }

    void UpdatePath()
    {
            //only call StartPath if the end of the path ahs allready been reached
            if(seeker.IsDone())
              seeker.StartPath(rb.position,target.position,OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        //resets path
        if(!p.error)
        {
            path=p;
            currentWaypoint =0;
        }
    }

    void FixedUpdate()
    {   
        //increments the swing timer
        lastSwing += Time.deltaTime;

        //check distance from enemy to player to stop crowding
        float toTarget = Vector2.Distance(rb.position, target.position);

        //resets path if no path or reached end of path and is far enough away from player
        if ((path==null || reachedEndofPath ==true) & stopChase < toTarget)
        {
            seeker.StartPath(rb.position,target.position,OnPathComplete);
            reachedEndofPath =false;
        }
        //check if reached end of path or to closer to player
        if(currentWaypoint >= path.vectorPath.Count || stopChase > toTarget)
        {
            //deal melee damage (add indicator)
            Attack(target.GetComponent<Destructible>());
            
            reachedEndofPath =true;
            return;
        }
        //havent reached end of path
        else
        {
            reachedEndofPath =false;
        }
        //use path to get vector of motion
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        //apply force to object in direction
        Vector2 force = direction * speed * Time.deltaTime;
        rb.AddForce(force);

        //check distance to next node on path
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        
        //check if move far enough to next node on path and update if so
        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
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