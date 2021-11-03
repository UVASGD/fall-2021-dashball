using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAi : MonoBehaviour
{
    /*
    public GameObject square1;
    public GameObject square2;
    public GameObject square3;
    public GameObject square4;
    public GameObject square5;
    public GameObject square6;
    public GameObject square7;
    public GameObject square8;
    public GameObject square9;
    public GameObject square10;
    public GameObject square11;
    public GameObject square12;
    public GameObject square13;
    public GameObject square14;
    public GameObject square15;
    public GameObject square16;
    public GameObject square17;
    public GameObject square18;
    public GameObject square19;
    public GameObject square20;
    public GameObject square21;
    public GameObject square22;
    public GameObject square23;
    public GameObject square24;
    public GameObject square25;
    public GameObject square26;
    public GameObject square27;
    public GameObject square28;
    public GameObject square29;
    public GameObject square30;
    public GameObject square31;
    public GameObject square32;
    */

    //0 = nothing, 1 = starting animation, 2 = shoot squares, 3 = shoot squares and move, 4 respawning squares
    //shot squares can be destroyed adn if they are leaves slot open 
    int phase = 0;
    int attackSquare = 2;
    [HideInInspector] public int hp = 4;
    //holds all squares used for boss room
    [HideInInspector] public List<GameObject> bossList = new List<GameObject>();
    bool firstGo = false;
    float holder;

    void Start()
    {
        phase = 1;
        //finds all squares in game space and adds them to list
        foreach (GameObject bossSqaure in GameObject.FindGameObjectsWithTag("bossSquare"))
        {
            bossList.Add(bossSqaure);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (holder > 2 && firstGo == false)
        {
            Phase1();
            firstGo = true;
        }
        else
            holder += Time.deltaTime;


        if (phase == 1 && bossList[attackSquare].GetComponent<boxBullet>().goNextShot())
        {
            Phase1();
        }

    }

    void Phase1()
    {
        //get random sqaure
        attackSquare = //3;
                        (Random.Range(0, bossList.Count-1));
        Debug.Log(attackSquare);

     //   bossList[attackSquare].GetComponent<SpriteRenderer>().color = Color.blue;

        bossList[attackSquare].GetComponent<boxBullet>().Shoot();
    }
}