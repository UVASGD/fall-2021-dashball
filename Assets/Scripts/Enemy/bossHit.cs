using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossHit : MonoBehaviour
{
    public GameObject TheBoss;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Ball")
        {
            TheBoss.GetComponent<bossAi>().hp -= 1;
            TheBoss.GetComponent<bossAi>().goPhase2();
            col.gameObject.GetComponent<Rigidbody2D>().velocity = //col.gameObject.GetComponent<Rigidbody2D>().velocity 
                                                                    new Vector2(0, 0);
            col.gameObject.GetComponent<Rigidbody2D>().transform.position = new Vector2(0, 0);
            foreach (GameObject bossSqaure in TheBoss.GetComponent<bossAi>().bossList)
            {
                if (bossSqaure.GetComponent<boxBullet>().destroyable == true)
                {
                    bossSqaure.GetComponent<boxBullet>().Respawn();
                }
            }
        }
    }

}
