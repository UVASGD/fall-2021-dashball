using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //inputs
    public Vector2 movement;
    public Vector2 aim;
    public bool fire;

    //im sorta copying this from last year's project im 90% sure some of it is not necessary
    Rigidbody2D rb2d;
    public float movePower = 5f;
    public float dashCD = 1f;
    public float lastDash = 0f;
    public float dashPower = 10f;
    public GameObject crosshairs;
    public float crosshairDistance = 4;
    public Sprite neutral;
    public Sprite dash;
    public Transform pos;

    //Animator Script 
    private SpriteRenderer spriteRenderer1;
    private SpriteRenderer spriteRenderer2;
    private SpriteRenderer spriteRenderer3;
    private SpriteRenderer spriteRenderer4;
    private CapsuleCollider2D capsule1;
    private CapsuleCollider2D capsule3;
    Animator anim1;
    Animator anim2;
    Animator anim3;
    Animator anim4;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim1 = GameObject.Find("Portal1").GetComponent<Animator>();
        anim2 = GameObject.Find("Portal2").GetComponent<Animator>();
        anim3 = GameObject.Find("Portal3").GetComponent<Animator>();
        anim4 = GameObject.Find("Portal4").GetComponent<Animator>();
        spriteRenderer1 = GameObject.Find("Portal1").GetComponent<SpriteRenderer>();
        spriteRenderer2 = GameObject.Find("Portal2").GetComponent<SpriteRenderer>();
        spriteRenderer3 = GameObject.Find("Portal3").GetComponent<SpriteRenderer>();
        spriteRenderer4 = GameObject.Find("Portal4").GetComponent<SpriteRenderer>();
        capsule1 = GameObject.Find("Portal1").GetComponent<CapsuleCollider2D>();
        capsule3 = GameObject.Find("Portal3").GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        aim = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        fire = Input.GetMouseButton(0);
    }

    void FixedUpdate() {
        
        //NOTE FOR FUTURE DEVELOPING:
        //to make a "dash" use rb2d.AddForce(new Vector2(speed,speed), ForceMode2D.Impulse)

        if(fire && lastDash >= dashCD){
            rb2d.velocity = new Vector2(0,0);
            rb2d.AddForce(aim.normalized * crosshairDistance * dashPower, ForceMode2D.Impulse);
            lastDash = 0f;
        }
        lastDash += Time.deltaTime;

        rb2d.AddForce(movement * movePower);

		Debug.DrawRay(transform.position, aim.normalized * crosshairDistance, Color.red);

		// position crosshairs
		//if(aim.magnitude < crosshairDistance) { 
			//crosshairs.transform.position = (Vector2) transform.position + aim;
		//} else {
		crosshairs.transform.position = (Vector2) transform.position + (aim.normalized * crosshairDistance);
		//}

		
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "Boost" ) {
            StartCoroutine(PowerUp(5f));            
            collision.gameObject.SetActive(false);
            Debug.Log("Speedi Boi");
        }
        if (collision.gameObject.name == "Portal1" ) {
            Vector2 tp = GameObject.Find("Portal2").transform.position;
            this.transform.position = tp;
            anim1.SetBool("out", true);
            StartCoroutine(disablePortal1(1.5f));
        }
        if (collision.gameObject.name == "Portal2" ) {
            anim2.SetBool("out", true);
            StartCoroutine(disablePortal2(1.5f));
        }
        if (collision.gameObject.name == "Portal3" ) {
            Vector2 tp = GameObject.Find("Portal4").transform.position;
            this.transform.position = tp;
            anim3.SetBool("out", true);
            StartCoroutine(disablePortal3(1.5f));
        }
        if (collision.gameObject.name == "Portal4" ) {
            anim4.SetBool("out", true);
            StartCoroutine(disablePortal4(1.5f));
        }
    }

    IEnumerator PowerUp(float duration) {
        movePower = 15f;
        yield return new WaitForSeconds(duration);
        movePower = 5f;
    }

    IEnumerator disablePortal1(float duration) {
        yield return new WaitForSeconds(duration);
        spriteRenderer1.enabled = false;
        capsule1.enabled = false;
    }

    IEnumerator disablePortal2(float duration) {
        yield return new WaitForSeconds(duration);
        spriteRenderer2.enabled = false;
    }

    IEnumerator disablePortal3(float duration) {
        yield return new WaitForSeconds(duration);
        spriteRenderer3.enabled = false;
        capsule3.enabled = false;
    }

    IEnumerator disablePortal4(float duration) {
        yield return new WaitForSeconds(duration);
        spriteRenderer4.enabled = false;
    }

}
