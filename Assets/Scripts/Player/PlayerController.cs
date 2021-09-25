using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //inputs
    public Vector2 pos;
    public Vector2 aim;
    private bool fire;

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
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        pos = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
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

        rb2d.AddForce(pos * movePower);

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
    }

    IEnumerator PowerUp(float duration) {
        movePower = 15f;
        yield return new WaitForSeconds(duration);
        movePower = 5f;
    }

}
