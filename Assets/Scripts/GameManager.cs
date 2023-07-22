using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float _gameLengthInSeconds = 10f;

    [SerializeField]
    private Text _ScoreText;

    [SerializeField]
    private Text _timerText;

    [SerializeField]
    private GameObject gameStateUI;

    private float _timer;
    private Text _gameStateText;
    private Animator _gameStateTextAnim;

    public static bool GameStarted = false;
    public static int Score = 0;
    public event System.Action GameStateChanged;

    // Start is called before the first frame update
    void Start()
    {
        _gameStateText = gameStateUI.GetComponent<Text>();
        _gameStateText.text = "Hit Space to play!";

        _gameStateTextAnim = gameStateUI.GetComponent<Animator>();
        _gameStateTextAnim.SetBool("ShowText", true);

        _timer = _gameLengthInSeconds;

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
            _timer -= Time.deltaTime;
            UpdateScoreBoard();
        }

        if (GameStarted && _timer <= 0)
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
        _gameStateTextAnim.SetBool("ShowText", false);
        // Notify the subscribers about game state change
        GameStateChanged?.Invoke();
    }

    private void UpdateScoreBoard()
    {
        _ScoreText.text = "Score:" + Score;
        _timerText.text = "Timer:" + Mathf.RoundToInt(_timer);
    }

    private void EndGame()
    {
        _gameStateText.text = "Game Over!\nPress Space to restart";
        _gameStateTextAnim.SetBool("ShowText", true);
        // Reset the game variables
        GameStarted = false;
        _timer = _gameLengthInSeconds;
        // Notify the subscribers about game state change
        GameStateChanged?.Invoke();
    }
}
