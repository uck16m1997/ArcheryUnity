using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioManager : BaseAudioManager
{

    [SerializeField]
    private AudioClip _gameStartClip;

    [SerializeField]
    private AudioClip _gameEndClip;
    private GameManager _gameManager;
    // Start is called before the first frame update
    new void Start()
    {
        // Get the audioSource using Base
        base.Start();
        // Get the GameManager and Subscribe
        _gameManager = GetComponent<GameManager>();
        _gameManager.GameStateChanged += OnEventHappened;
    }

    protected override void OnEventHappened()
    {
        if (GameManager.GameStarted)
        {
            PlayClip(_gameStartClip);
        }
        else
        {
            PlayClip(_gameEndClip);
        }
    }

}
