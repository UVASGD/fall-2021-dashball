using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 pos;
    public Vector2 aim;
    private bool fire;
    private bool w;
    private bool a;
    private bool s;
    private bool d;
    Rigidbody2D rb2d;
    public float acceleration = 1f;
    public float dashCD = 1f;
    private float lastDash = 0f;
    public float dashPower = 10f;
    public GameObject crosshairs;
    public float crosshairDistance = 4;
    public Sprite neutral;
    public Sprite dash;
    public SpriteRenderer spriteRenderer;
    public float maxSpeed = 10f;
    public float boostmultiplier = 1.5f;
    public float boostduration = 3f;
    public float maxSpeedmult = 1.5f;
    
    public static bool MagnetCollide = false;
    public static bool RecallActive = false;
    public float magnetduration = 8f;
    private float timer = 0f;

    void Start()
    {
       rb2d = GetComponent<Rigidbody2D>(); 
    }

    void Update()
    {
        pos = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        aim = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        fire = Input.GetMouseButton(0);
        w = Input.GetKey("w");
        a = Input.GetKey("a");
        s = Input.GetKey("s");
        d = Input.GetKey("d");
    }

    void FixedUpdate()
    {

        if(MagnetCollide==true){
            timer += Time.deltaTime;
            if(timer>=magnetduration)
                MagnetCollide = false;
    
        }

        if(fire && lastDash >= dashCD){
            rb2d.velocity += aim.normalized * dashPower;
            lastDash = 0f;
        }
         
        if (w){
            rb2d.velocity += new Vector2(0,acceleration);
        }

        if (a){
            rb2d.velocity += new Vector2(-acceleration,0);
        }

        if (s){
            rb2d.velocity += new Vector2(0,-acceleration);
        }

        if (d){
            rb2d.velocity += new Vector2(acceleration,0);
        }

        if(rb2d.velocity.magnitude > maxSpeed){
                rb2d.velocity = rb2d.velocity.normalized * maxSpeed;
        }
  
        lastDash += Time.deltaTime;

		Debug.DrawRay(transform.position, aim.normalized * crosshairDistance, Color.red);

		crosshairs.transform.position = (Vector2) transform.position + (aim.normalized * crosshairDistance);
	
    }

   private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "Boost" ) {
            StartCoroutine(PowerUp(boostduration));            
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.name == "Magnet" ) {
            MagnetCollide = true;        
            collision.gameObject.SetActive(false);
        }
<<<<<<< Updated upstream
        if (collision.gameObject.name == "Recall" ) {
            RecallActive = true;
=======
        if (collision.gameObject.name == "Relocator" ) {
                   
>>>>>>> Stashed changes
            collision.gameObject.SetActive(false);
        }
    }

    IEnumerator PowerUp(float boostduration) {
        dashPower = dashPower * boostmultiplier;
        maxSpeed = maxSpeed * maxSpeedmult;
        yield return new WaitForSeconds(boostduration);
        dashPower = dashPower/ boostmultiplier;
        maxSpeed = maxSpeed * maxSpeedmult;
    }

}
