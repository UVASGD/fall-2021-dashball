using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 pos;
    public Vector2 aim;
    private bool fire;
    Rigidbody2D rb2d;
    public float acceleration = 1f;
    public float dashCD = 1f;
    public float lastDash = 0f;
    public float dashPower = 10f;
    public GameObject crosshairs;
    public float crosshairDistance = 4;
    public Sprite neutral;
    public Sprite dash;
    public SpriteRenderer spriteRenderer;
    public float maxSpeed = 10f;

    void Start()
    {
       rb2d = GetComponent<Rigidbody2D>(); 
    }

    void Update()
    {
        pos = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        aim = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        fire = Input.GetMouseButton(0);
    }

    void FixedUpdate()
    {

         if(fire && lastDash >= dashCD){
            rb2d.velocity += aim.normalized * dashPower;
            lastDash = 0f;
        }
        
              
            if (Input.GetKey("w")){
                rb2d.velocity += new Vector2(0,acceleration);
                if(rb2d.velocity.magnitude > maxSpeed){
                rb2d.velocity = rb2d.velocity.normalized * maxSpeed;
            } 
            }
            if (Input.GetKey("a")){
                rb2d.velocity += new Vector2(-acceleration,0);
                if(rb2d.velocity.magnitude > maxSpeed){
                rb2d.velocity = rb2d.velocity.normalized * maxSpeed;
            } 
            }
            if (Input.GetKey("s")){
                rb2d.velocity += new Vector2(0,-acceleration);
                if(rb2d.velocity.magnitude > maxSpeed){
                rb2d.velocity = rb2d.velocity.normalized * maxSpeed;
            } 
            }
            if (Input.GetKey("d")){
                rb2d.velocity += new Vector2(acceleration,0);
                if(rb2d.velocity.magnitude > maxSpeed){
                rb2d.velocity = rb2d.velocity.normalized * maxSpeed;
            } 
            }
            
    
      
        lastDash += Time.deltaTime;

        

		Debug.DrawRay(transform.position, aim.normalized * crosshairDistance, Color.red);

		crosshairs.transform.position = (Vector2) transform.position + (aim.normalized * crosshairDistance);
	

    }

   private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "Boost" ) {
            StartCoroutine(PowerUp(5f));            
            collision.gameObject.SetActive(false);
            Debug.Log("Speedi Boi");
        }
    }

    IEnumerator PowerUp(float duration) {
        acceleration = 2f;
        yield return new WaitForSeconds(duration);
        acceleration = 2f;
    }
}
