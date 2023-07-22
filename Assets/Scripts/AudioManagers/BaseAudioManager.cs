using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    // Start is called before the first frame update
    protected void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    abstract protected void OnEventHappened();



    protected void PlayAudioSource()
    {
        audioSource.Play();
    }
    protected void StopAudioSource()
    {
        audioSource.Stop();
    }

    protected void PlayClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
