using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxBullet : MonoBehaviour
{

    public int damage = 15;
    float distance = 0.0f;
    Rigidbody2D rb;
    //1 - down, 2 = left, 3 = up, 4 = right
    public int direction;
    public bool destroyable = false;
    Vector3 loc;

    float xcord;
    float ycord;

    float movePower = 0.15f;

    bool comeBack = false;

    bool goNext = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        loc = rb.position;
        xcord = rb.transform.position.x;
        ycord = rb.transform.position.y;
    }

    void FixedUpdate()
    {
        if (!CheckStop())
        {
            distance += Vector3.Distance(loc, rb.position);
            loc = rb.position;

            if (distance > 5.6f)
                comeBack = true;
            if (direction == 1)
                if (comeBack)
                    rb.AddForce(new Vector2(0, 10) * movePower);
                else
                    rb.AddForce(new Vector2(0, -10) * movePower);
            else if (comeBack)
                if (distance >= 1.0f)
                    rb.AddForce(new Vector2(10, 0) * movePower);
                else
                    rb.AddForce(new Vector2(-10, 0) * movePower);
            else if (direction == 3)
                if (comeBack)
                    rb.AddForce(new Vector2(0, -10) * movePower);
                else
                    rb.AddForce(new Vector2(0, 10) * movePower);
            else
                if (comeBack)
                rb.AddForce(new Vector2(-10, 0) * movePower);
            else
                rb.AddForce(new Vector2(-10, 0) * movePower);
        }
    }


    bool CheckStop()
    {
        if (direction == 1 && rb.transform.position.y >= ycord && comeBack == true && goNext == false)
        {
            destroyable = false;
            comeBack = false;
            rb.velocity = new Vector2(0, 0);
            gameObject.transform.position = (new Vector2(xcord, ycord));
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            goNext = true;
            distance = 0;
            return true;
        }
        else if (direction == 2 && rb.transform.position.x >= xcord && comeBack == true && goNext == false)
        {
            destroyable = false;
            comeBack = false;
            rb.velocity = new Vector2(0, 0);
            gameObject.transform.position = (new Vector2(xcord, ycord));
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            goNext = true;
            distance = 0;            
            return true;
        }
        else if (direction == 3 && rb.transform.position.y <= ycord && comeBack == true && goNext == false)
        {
            destroyable = false;
            comeBack = false;
            rb.velocity = new Vector2(0, 0);
            gameObject.transform.position = (new Vector2(xcord, ycord));
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            goNext = true;
            distance = 0;            
            return true;
        }
        else if (direction == 4 && rb.transform.position.x <= xcord && comeBack == true && goNext == false)
        {
            destroyable = false;
            comeBack = false;
            rb.velocity = new Vector2(0, 0);
            gameObject.transform.position = (new Vector2(xcord, ycord));
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            goNext = true;
            distance = 0;            
            return true;
        }
        return false;
    }

    public bool goNextShot()
    {
        if(goNext)
        {
            goNext = false;
            return true;
        }
        return false;
    }

    public void Shoot()
    {
        goNext = false;
        //unlock position
        rb.constraints = RigidbodyConstraints2D.None;
        //decide velocity to go into center and freeze nessecarry positions
        if (direction == 1)
        {

            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            rb.AddForce(new Vector2(0, -10) * movePower);
        }
        else if (direction == 2)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            rb.AddForce(new Vector2(-10, 0) * movePower);
        }
        else if (direction == 3)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            rb.AddForce(new Vector2(0, 10) * movePower);
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            rb.AddForce(new Vector2(10, 0) * movePower);
        }
        //   bossList[attackSquare].GetComponent<SpriteRenderer>().color = new Color (1, 0, 0, 1); ;
        destroyable = true;
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        //for somereason really hates camerabounds of unmoving camera
        if (target.gameObject.name != "CameraBounds")
        {
            //checks to see if hit player
            if (target.gameObject.name.Equals("Player"))
            {
                Attack(target.gameObject.GetComponent<Destructible>());
                //removes object
                if (destroyable)
                    Destroy(gameObject);
            }
            else if (target.gameObject.name.Equals("Ball"))
                if (destroyable)
                    Destroy(gameObject);
        }
    }
    //deals damage based on bullet damage
    private void Attack(Destructible target)
    {
        target.TakeDamage(damage);
    }
}
