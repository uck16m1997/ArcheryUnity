using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAudioManager : BaseAudioManager
{

    private TargetBehaviour _targetBehaviour;

    new void Start()
    {
        // Get the audioSource using Base
        base.Start();
        // Get the target behaviour and Subscribe
        _targetBehaviour = GetComponent<TargetBehaviour>();
        _targetBehaviour.GotHit += OnEventHappened;
    }

    protected override void OnEventHappened()
    {
        // Base class will play the audio source
        PlayAudioSource();
    }

}
