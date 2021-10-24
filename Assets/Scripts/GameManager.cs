using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //gameObjects
    public GameObject ball;
    public Vector3 ballStart;
    public GameObject player;
    public Vector3 playerStart;

    public bool isActive = false;
    public Button startButton;


    // Start is called before the first frame update
    void Start()
    {
        ballStart = ball.transform.position;
        playerStart = player.transform.position;
        startButton.gameObject.GetComponent<Button>();
        startButton.onClick.AddListener(StartGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Start the game
    public void StartGame() {
        isActive = true;
        startButton.gameObject.SetActive(false);
    }

    public void ResetGame(){
        //temp for demo, resets positions
        ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        ball.transform.position = ballStart;
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        player.transform.position = playerStart;
    }
}
