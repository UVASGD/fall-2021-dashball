using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballscript : MonoBehaviour
{
    public GameManager gm;

    //This is for sticky buttons to hold ball in place until player hit
    public GameObject tiedStickyButton = null;

Rigidbody2D ballrb;
public GameObject player;
public float magnetspeed = 5f;

    // Start is called before the first frame update
    void Start()
    {

        ballrb = GetComponent<Rigidbody2D>();

        //If game manager not specified, find it
        if (!gm)
            gm = GameObject.FindObjectOfType<GameManager>();

        
        //Changed this so you dont need to find name of game manager anymore, just the component
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
    Vector2 dir = (this.transform.position - player.transform.position).normalized;

        if(PlayerMovement.MagnetCollide==true){
            ballrb.AddForce(-dir*magnetspeed);
        }
        if(PlayerMovement.RecallActive==true){
            ballrb.velocity = new Vector2(0, 0);
            transform.position =  new Vector2(player.transform.position.x, player.transform.position.y -1.5f);
            PlayerMovement.RecallActive = false;
        }
        
        
        //If its on a sticky button
        if (tiedStickyButton)
        {
            //Then slow down the ball and stick it to center
            transform.position = Vector3.Lerp(transform.position, tiedStickyButton.transform.position, Time.fixedDeltaTime * 5);
            GetComponent<Rigidbody2D>().velocity *= 0.8f;
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.name == "Goal"){
            gm.ResetGame();
            //temp for demo, will need to move to next scene, see gamemanager
        }
        if (col.gameObject.name == "Portal1" ) {
            Vector2 tp = GameObject.Find("Portal2").transform.position;
            this.transform.position = tp;
        }
        //First tests to see if collided with button, THEN tests to see if its a sticky button
        else if (col.gameObject.GetComponent<LogicActivator>() && col.gameObject.GetComponent<LogicActivator>().type == 3)
        {
            tiedStickyButton = col.gameObject;
        }
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If player hit
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            tiedStickyButton = null;
        }

    }
}
