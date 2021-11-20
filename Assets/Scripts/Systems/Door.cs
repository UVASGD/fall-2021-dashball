using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public LogicActivator[] activators = new LogicActivator[1]; //Attach buttons/switches here
    public bool[] inverted; //Should be the same sized list as activators.  This tells unity whether a button should be on or not
    Vector2 s;
    public float doorSpeed = 15;
    public bool onlyOneNeeded = false;

    private void Start()
    {
        s = transform.localScale;
        inverted = new bool[activators.Length];
    }

    private void FixedUpdate()
    {
        //If you kinda forgot to put in the activator, i gotchu covered bro
        if (activators.Length == 0 || !activators[0])
        {
            Debug.LogWarning(gameObject + " does not have any logic activators plz fix");
            return;
        }

        //If you only need one button to activate, then use your big boi voice and tell unity
        bool okayAreWeGucci = true;
        if (onlyOneNeeded)
            okayAreWeGucci = false;

        //Okay gamers time to go through every single one of these buttons to see if we are big brain enough to open a door
        for (int i = 0; i < activators.Length; i++)
        {
            if (!onlyOneNeeded)
            {
                //If the button is on and inverted or if the button is off and not inverted
                if ((activators[i].on && inverted[i]) || (!activators[i].on && !inverted[i]))
                {
                    okayAreWeGucci = false;
                    break;
                }
            }
            else
            {
                //If the button is on and not inverted or if the button is off and inverted
                if ((activators[i].on && !inverted[i]) || (!activators[i].on && inverted[i]))
                {
                    okayAreWeGucci = true;
                    break;
                }
            }
        }


        //
        //
        //Below here is what you would want to modify if you wanted to add something other than doors
        //
        //


        if (okayAreWeGucci)
        {
            //We're in
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(s.x, 0), Time.fixedDeltaTime * doorSpeed);
        }
        else
        {
            //We're in't
            transform.localScale = Vector2.Lerp(transform.localScale, s, Time.fixedDeltaTime * doorSpeed);
        }
    }
}
