using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float gameLengthInSeconds = 10f;

    [SerializeField]
    private Text ScoreText;

    [SerializeField]
    private Text timerText;

    [SerializeField]
    private GameObject gameStateUI;

    private float timer;
    private Text gameStateText;
    private Animator gameStateTextAnim;

    public static bool GameStarted = false;
    public static int Score = 0;
    public event System.Action GameStateChanged;

    // Start is called before the first frame update
    void Start()
    {
        gameStateText = gameStateUI.GetComponent<Text>();
        gameStateText.text = "Hit Space to play!";

        gameStateTextAnim = gameStateUI.GetComponent<Animator>();
        gameStateTextAnim.SetBool("ShowText", true);

        timer = gameLengthInSeconds;

        UpdateScoreBoard();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }

        if (GameStarted)
        {
            timer -= Time.deltaTime;
            UpdateScoreBoard();
        }

        if (GameStarted && timer <= 0)
        {
            EndGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void StartGame()
    {
        Score = 0;
        GameStarted = true;
        gameStateTextAnim.SetBool("ShowText", false);
        // Notify the subscribers about game state change
        GameStateChanged?.Invoke();
    }

    private void UpdateScoreBoard()
    {
        ScoreText.text = "Score:" + Score;
        timerText.text = "Timer:" + Mathf.RoundToInt(timer);
    }

    private void EndGame()
    {
        gameStateText.text = "Game Over!\nPress Space to restart";
        gameStateTextAnim.SetBool("ShowText", true);
        // Reset the game variables
        GameStarted = false;
        timer = gameLengthInSeconds;
        // Notify the subscribers about game state change
        GameStateChanged?.Invoke();
    }
}
