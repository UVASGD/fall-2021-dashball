using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    public GameManager gm;
    public PlayerController pc;

    //magnet
    public Rigidbody2D ballrb;
    public float magnetSpeed;

    //attack properties
    public float damage = 25;
    public float swingTimer = 1; //ie how long between "attacks" (just so it doesnt do a lot of little attacks, may have to modify this system)
    public float lastSwing = 0; //the actual timer maybe I should use different names lol

    //This is for sticky buttons to hold ball in place until player hit
    public GameObject tiedStickyButton = null;

    // Start is called before the first frame update
    void Start()
    {
        ballrb = GetComponent<Rigidbody2D>();
        pc = gm.player.GetComponent<PlayerController>();
        //If game manager not specified, find it
        if (!gm)
            gm = GameObject.FindObjectOfType<GameManager>();
        //Changed this so you dont need to find name of game manager anymore, just the component
    }

    // Update is called once per frame
    void Update()
    {
        lastSwing += Time.deltaTime;
    }
    private void FixedUpdate()
    {
        Vector2 dir = (this.transform.position - gm.player.transform.position).normalized;

        if(Input.GetKey("space")){
            ballrb.AddForce(-dir*magnetSpeed);
        }

        if(pc.RecallActive==true){
            ballrb.velocity = new Vector2(0, 0);
            transform.position =  new Vector2(gm.player.transform.position.x, gm.player.transform.position.y -1.5f);
            pc.RecallActive = false;
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
        if (col.gameObject.name == "Goal") {
			this.transform.position = new Vector3(-10, 0, -1);
			SceneManager.LoadScene("Level2");
		}
		if (col.gameObject.name == "2 to 3") {
			SceneManager.LoadScene("Level3");
		}
        if (col.gameObject.name == "3 to 4") {
			SceneManager.LoadScene("Level4");
		}
        if (col.gameObject.name == "4 to win") {
			SceneManager.LoadScene("Victory");
		}
		// if (collision.CompareTag("3 to win")) {
		// 	gm.isActive = false;
		// 	SceneManager.LoadScene("Victory");
		// 	Destroy(gameObject);			
		// }

        //First tests to see if collided with button, THEN tests to see if its a sticky button
        else if (col.gameObject.GetComponent<LogicActivator>() && col.gameObject.GetComponent<LogicActivator>().type == 3)
        {
            tiedStickyButton = col.gameObject;
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        //If player hit
        if (col.gameObject.GetComponent<PlayerController>())
        {
            tiedStickyButton = null;
        }

        //if a non-player destructible is hit
        else if(col.gameObject.GetComponent<Destructible>()){
            Attack(col.gameObject.GetComponent<Destructible>());
        }

    }

    private void Attack(Destructible target){
        if (lastSwing >= swingTimer){
            target.TakeDamage(damage);
            lastSwing = 0;
        }
    }

}
