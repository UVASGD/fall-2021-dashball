using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    //gameObjects
    public GameObject ball;
    public Vector3 ballStart;
    public GameObject player;
    public Vector3 playerStart;

    public bool isActive = false;

    // UI Elements
    public Button startButton;
    public TextMeshProUGUI levelCoverText;
    public Button exitButton;
    public Button controlButton;
    public Button aboutButton;

    // Start is called before the first frame update
    void Start()
    {
        ballStart = ball.transform.position;
        playerStart = player.transform.position;
        
        //these try/catches are for debug scenes w/o a startbutton
        try{
        startButton.gameObject.GetComponent<Button>();
        startButton.onClick.AddListener(StartGame);
        }
        catch{
            StartGame();
        }
        //these try/catches or for non-start games wihtout exit/control buttons
        try {
            exitButton.gameObject.GetComponent<Button>();
            exitButton.onClick.AddListener(ExitGame);
            controlButton.gameObject.GetComponent<Button>();
            aboutButton.gameObject.GetComponent<Button>();
        }
        catch {

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Start the game
    public void StartGame() {
        isActive = true;
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "StartMenu") {
		    SceneManager.LoadScene("Level1");
        }
        //these are for debug scenes
        if(startButton){
            startButton.gameObject.SetActive(false);
        }
        if(levelCoverText){
            levelCoverText.gameObject.SetActive(false);
        }
    }

    public void ResetGame(){
        //temp for demo, resets positions
        ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        ball.transform.position = ballStart;
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        player.transform.position = playerStart;
    }

    public void ExitGame() {
        Application.Quit();
    }
}
