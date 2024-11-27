using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public AudioClip backgroundMusicClip; // The background music clip
    public float volume = 0.5f;           // Volume for background music
    private AudioSource audioSource;      // AudioSource for background music

    void Start()
    {
        // Ensure the background music clip is assigned
        if (backgroundMusicClip != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();  // Add an AudioSource component if it doesn't exist
            audioSource.clip = backgroundMusicClip;                // Assign the background music clip
            audioSource.loop = true;                                // Loop the music
            audioSource.volume = volume;                            // Set the volume
            audioSource.Play();                                     // Start playing the background music
        }
        else
        {
            Debug.LogWarning("Background music clip is not assigned!");
        }
    }
}
