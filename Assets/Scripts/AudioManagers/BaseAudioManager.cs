using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAudioManager : MonoBehaviour
{
    private AudioSource _audioSource;

    // Start is called before the first frame update
    protected void Start()
    {
        _audioSource = GetComponent<AudioSource>();

    }

    abstract protected void OnEventHappened();



    protected void PlayAudioSource()
    {
        _audioSource.Play();
    }
    protected void StopAudioSource()
    {
        _audioSource.Stop();
    }

    protected void PlayClip(AudioClip clip)
    {
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
