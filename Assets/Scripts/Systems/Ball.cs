using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameManager gm;

    //This is for sticky buttons to hold ball in place until player hit
    public GameObject tiedStickyButton = null;

    // Start is called before the first frame update
    void Start()
    {
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
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            tiedStickyButton = null;
        }

    }
}
