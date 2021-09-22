using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAi : MonoBehaviour
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
    public Transform target;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //simple move test
        //transform.position = new Vector3 (transform.position.x + xSpeed, transform.position.y + ySpeed);
        
               //simple move towrds player
                if (Vector3.Distance(transform.position,target.position)>distance)
                    transform.position = Vector3.MoveTowards(transform.position, target.position, speed);  
    }
}
