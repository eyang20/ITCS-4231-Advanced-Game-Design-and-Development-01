using UnityEngine;

public class ShootSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;
    public float volume = 1f;

    public void PlaySound()
    {
        //Plays the audio from audioSource with the sound being clip and volume based on the float value volume.
        audioSource.PlayOneShot(clip, volume);
    }
}