using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PlayerController : Destructible
{

    //TODOS FROM PlayerMovement.cs
    //Magnet powerup
    //max speed (both for dash and normal, consider a deacceleration of maxSpeed after a dash?)
    //delete PlayerMovement once done

    //inputs
    public Vector2 movement;
    public Vector2 aim;
    public bool fire;

    public GameManager gm;

    public Animator animator;

    //im sorta copying this from last year's project im 90% sure some of it is not necessary
    Rigidbody2D rb2d;
    public float currentMaxSpeed;
    public float maxMoveSpeed = 10f;
    public float maxDashSpeed = 20f;
    public float movePower = 5f;
    public float dashCD = 1f;
    public float lastDash = 0f;
    public float dashPower = 10f;
    public GameObject crosshairs;
    public float crosshairDistance = 4;
    public Sprite neutral;
    public Sprite dash;
    public Transform pos;

    //power ups/commnads
    public bool RecallActive = false;

    // Bool portal
    public bool ballEntered1 = false;
    public bool ballEntered2 = false;

    //DEATH
    public float timeToDie;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        RecallActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.isActive) {
            movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            aim = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            fire = Input.GetMouseButton(0);  
            InvokeRepeating("RegenerateStamina", 0f, .5f);
        }

    }

    void FixedUpdate() {
        
        //NOTE FOR FUTURE DEVELOPING:
        //to make a "dash" use rb2d.AddForce(new Vector2(speed,speed), ForceMode2D.Impulse)

        if(fire && lastDash >= dashCD){
            StartCoroutine(Deccelerate());
            rb2d.velocity = new Vector2(0,0);
            rb2d.AddForce(aim.normalized * crosshairDistance * dashPower, ForceMode2D.Impulse);
            lastDash = 0f;
        }
        lastDash += Time.deltaTime;

        rb2d.AddForce(movement * movePower);
        //rb2d.velocity += (.00000001 + movement) * movePower;
        //if(Vector2.magnitude(rb2d.velocity) > maxSpee)
        if(rb2d.velocity.magnitude > currentMaxSpeed){
            //note: using velocity makes it easily push physics objects away instead of bouncing off of them (as intended)2
                rb2d.velocity = rb2d.velocity.normalized * currentMaxSpeed;
        }

		Debug.DrawRay(transform.position, aim.normalized * crosshairDistance, Color.red);

        Vector2 dir = rb2d.velocity;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        animator.SetFloat("Angle", angle);
        animator.SetFloat("VelMag", rb2d.velocity.magnitude);
		// position crosshairs
		//if(aim.magnitude < crosshairDistance) { 
			//crosshairs.transform.position = (Vector2) transform.position + aim;
		//} else {
		crosshairs.transform.position = (Vector2) transform.position + (aim.normalized * crosshairDistance);
		//}

		
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "Boost" ) {
            StartCoroutine(PowerUp(10f));            
            collision.gameObject.SetActive(false);
            Debug.Log("Speedi Boi");
        }

        if (collision.gameObject.name == "Recall" ) {
            RecallActive = true;
            collision.gameObject.SetActive(false);
        }
        
    }

    IEnumerator PowerUp(float duration) {
        movePower += 5f;
        maxMoveSpeed += 5f;
        maxDashSpeed += 5f;
        currentMaxSpeed += 5f;
        yield return new WaitForSeconds(duration);
        maxMoveSpeed -= 5f;
        maxDashSpeed -= 5f;
        currentMaxSpeed -= 5f;
        movePower -= 5f;
    }

    IEnumerator Deccelerate() {
        currentMaxSpeed = maxDashSpeed;
        while (currentMaxSpeed > maxMoveSpeed){
            yield return new WaitForSeconds(0.05f);
            currentMaxSpeed -= 1f;
        }
        currentMaxSpeed = maxMoveSpeed;
        
    }

    public override void Die() {
        animator.SetBool("Dying", true);
		StartCoroutine(StartDying());
    }

    IEnumerator StartDying(){
        yield return new WaitForSeconds(timeToDie);
        gm.isActive = false;
        SceneManager.LoadScene("Defeat");
    }

    public override void TakeDamage(float amount)
    {
        this.hitPoints -= amount;
		gm.UpdateHealth(hitPoints);
		// AudioSource.PlayClipAtPoint(clips[Random.Range(0,2)], transform.position);
        if (hitPoints <= 0)
        {
            Die();
        }
    }

    public void RegenerateStamina() {
        gm.UpdateStamina(lastDash);
    }
}
