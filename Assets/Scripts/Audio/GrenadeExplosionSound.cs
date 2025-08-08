using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeExplosionSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;
    public float volume = 1f;

    public void PlaySound()
    {
        //Plays the audio from audioSource.
        audioSource.PlayOneShot(clip, volume);
    }
}
