using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBoundsBoss : MonoBehaviour
{
    public GameObject TheBoss;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "player")
        {
            col.gameObject.GetComponent<Rigidbody2D>().velocity = col.gameObject.GetComponent<Rigidbody2D>().velocity * -1;
        }
    }
}
