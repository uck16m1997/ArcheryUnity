using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioManager : BaseAudioManager
{

    [SerializeField]
    private AudioClip gameStartClip;

    [SerializeField]
    private AudioClip gameEndClip;
    private GameManager gameManager;
    // Start is called before the first frame update
    new void Start()
    {
        // Get the audioSource using Base
        base.Start();
        // Get the GameManager and Subscribe
        gameManager = GetComponent<GameManager>();
        gameManager.GameStateChanged += OnEventHappened;
    }

    protected override void OnEventHappened()
    {
        if (GameManager.GameStarted)
        {
            PlayClip(gameStartClip);
        }
        else
        {
            PlayClip(gameEndClip);
        }
    }

}
