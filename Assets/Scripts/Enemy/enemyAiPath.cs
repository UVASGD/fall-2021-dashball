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
        //do nothing if no path
        if (path==null || reachedEndofPath ==true)
        return;
        //check if reached end of path
        if(currentWaypoint >= path.vectorPath.Count)
        {
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
}
