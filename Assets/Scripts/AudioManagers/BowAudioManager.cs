using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowAudioManager : BaseAudioManager
{

    // Start is called before the first frame update
    new void Start()
    {
        // Get the audioSource using Base
        base.Start();
        // Subscribe to events that will start and stop audio
        BowController.Shooting.BowDrawing += OnEventHappened;
        BowController.Shooting.BowReleased += OnEventStopped;
        BowController.Idle.BowIdle += OnEventStopped;

    }

    protected override void OnEventHappened()
    {
        PlayAudioSource();
    }
    protected void OnEventStopped()
    {
        StopAudioSource();
    }
}
