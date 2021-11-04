using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAi : MonoBehaviour
{
    //0 = nothing, 1 = starting animation, 2 = shoot squares, 3 = shoot squares and move, 4 respawning squares
    //shot squares can be destroyed adn if they are leaves slot open 
    int phase = 0;
    int attackSquare = 2;
    [HideInInspector] public int hp = 4;
    //holds all squares used for boss room
    [HideInInspector] public List<GameObject> bossList = new List<GameObject>();
    bool firstGo = false;
    float holder;
    public List<int> boxNumbers = new List<int>();
    public List<int> numbersHolder = new List<int>();

    public List<int> toRemove = new List<int>();

    void Start()
    {
        phase = 1;
                //2;
        //finds all squares in game space and adds them to list
        foreach (GameObject bossSqaure in GameObject.FindGameObjectsWithTag("bossSquare"))
        {
            bossList.Add(bossSqaure);
        }
        for (int i = 0; i < 28; i++)
            boxNumbers.Add(i);
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
        else if (phase == 2 && firstGo == true)
        {
            if (checkShots())
                if (numbersHolder.Count == 0)
                    Phase2();
        }

    }

    public void goPhase2()
    {
        phase=2;
    }

    bool checkShots()
    {
        foreach (int count in toRemove)
        {
            numbersHolder.Remove(count);
        }
        toRemove = new List<int>();
        foreach (int count in numbersHolder)
        {
            if (bossList[count].GetComponent<boxBullet>().goNextShot())
            {
                boxNumbers.Add(count);
                toRemove.Add(count);
            }
            else
                return false;
        }
        return true;
    }

    void Phase2()
    {
        int holder;
        int shooter;
        for (int i = 0; i < 4 - hp + 1; i++)
        {
            holder = (Random.Range(0, boxNumbers.Count - 1));
            shooter = boxNumbers[holder];
            numbersHolder.Add(shooter);
            boxNumbers.Remove(holder);
            bossList[shooter].GetComponent<boxBullet>().Shoot();
        }
    }

    void Phase1()
    {
        //get random sqaure
        attackSquare = //3;
                        (Random.Range(0, bossList.Count - 1));
        Debug.Log(attackSquare);

        //   bossList[attackSquare].GetComponent<SpriteRenderer>().color = Color.blue;

        bossList[attackSquare].GetComponent<boxBullet>().Shoot();
    }
}