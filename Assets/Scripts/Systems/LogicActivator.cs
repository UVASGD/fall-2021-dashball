using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicActivator : MonoBehaviour
{
    //All button/switch logic stored here
    //Click and drag the button gameobject out of the prefab folder to make more of these bad bois
    public Animator animator;

    public bool on = false;
    public int type = 0; //0 = red button (remains active even when stepped off), 1 = blue button (deactivates if stepped off), 2 = switch (step on to toggle on/off), 3 = sticky
    Color c;

    private void Start()
    {
        //c = GetComponent<SpriteRenderer>().color;
        animator = GetComponent<Animator>();
        animator.SetBool("On", on);
    }

    //Called whenever something enters the button/switch
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the ball has collided with the button
        if (collision.gameObject.GetComponent<LogicInteractable>())
        {
            //If this is a switch
            if (type == 2)
            {
                //Toggle activate state
                on = !on;
                animator.SetBool("On", on);

                if (on)
                {
                    /*Color col = c;
                    col.r += 30;
                    col.b += 30;
                    col.g += 30;
                    GetComponent<SpriteRenderer>().color = col;*/
                }
                else
                {
                    //GetComponent<SpriteRenderer>().color = c;
                }
            }
            else
            {
                on = true;
                animator.SetBool("On", on);

                /*Color col = c;
                col.r += 10;
                col.b += 10;
                col.g += 10;
                GetComponent<SpriteRenderer>().color = col;*/
            }
        }
    }
    //Called whenever something leaves the button/switch
    private void OnTriggerExit2D(Collider2D collision)
    {
        //If not a red button
        if (type != 0 && collision.gameObject.GetComponent<LogicInteractable>())
        {
            //If blue button or sticky
            if (type == 1 || type == 3)
            {
                on = false;
                animator.SetBool("On", on);
                //GetComponent<SpriteRenderer>().color = c;
            }
        }
    }
}
