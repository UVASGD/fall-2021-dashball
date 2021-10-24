using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class enemyAiBound : MonoBehaviour
{
    //controls speed at which enemy moves
    public float speed = 2.0f;
    //set to wall to hug
    public Transform wall;
    //direction the enemy is facing
    int dir = 0;
    //angle the enenmy moves
    float angle = 0.0f;

    //the point at which the object is bound to the wall
    public Transform boundPoint;

    //foward point used for raycasting
    public Transform movePoint;
    // Update is called once per frame
    void Update()
    {

               //simple move along wall
              //  if (Vector3.Distance(transform.position,wall.position)>distance)
      Debug.Log(" enemy");                  
                    RaycastHit hit; 
                      Vector3 fwd = movePoint.transform.TransformDirection(Vector3.forward);
                    
                    Debug.DrawRay(movePoint.transform.position, fwd * 50, Color.green);
  if (Physics.Raycast(movePoint.transform.position, fwd, out hit, 50))
  {
      Debug.Log("Close to enemy");
  }
/*                    
                    Ray ray =  Physics.Raycast(inc,Vector3.right, 1.0f);
                    
                    if(hit.collider.tag == "levelTile")
                    {
                        float angle= Vector3.Angle(Vector3(hit.normal.x, hit.normal.y, hit.normal.z),hit);
                        Debug.Log(angle);
                        //transform.Rotate(0, Input.GetAxis("Horizontal")*turnSpeed*Time.deltaTime, 0);
    
                    }


                    if(boundPoint)
                    transform.position = new Vector2(transform.position.x + movespeed * Time.deltaTime, transform.position.y);
    */
    }
}
