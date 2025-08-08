using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClipArray;
    public float timeBetweenSteps = 0.25f;
    float timer;

    AudioClip lastClip;

    public void PlayClip()
    {
        //The value timer is added by Time.deltaTime.
        timer += Time.deltaTime;

        //If timer is greater than timeBetweenSteps...
        if (timer > timeBetweenSteps)
        {
            //...call audioSource and get it to play a one shot clip of the method RandomClip() and then set timer to zero.
            audioSource.PlayOneShot(RandomClip());
            timer = 0;
        }
    }

    public AudioClip RandomClip()
    {
        //return audioClipArray as a different clip through a random range staring from zero from the audioClipArray's length minus 1.
        return audioClipArray[Random.Range(0, audioClipArray.Length - 1)];
    }
}
