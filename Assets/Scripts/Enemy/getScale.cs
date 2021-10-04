using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getScale : MonoBehaviour
{
    //enemy to use range of
    public Transform enemyBase;

    void Start()
    {
        //gets value from enemy it is assigned too and places in vector
        float hold = enemyBase.GetComponent<enemyAiPath>().giveScale();   
        Vector3 scaling = new Vector3(hold, hold, hold);
        //gives vector to scale object
        gameObject.transform.localScale = scaling;
    }
}
