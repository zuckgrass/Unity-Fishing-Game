using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public AudioClip backgroundMusicClip;  // Assign this in the Inspector
    public float volume = 0.5f;            // Set your desired volume level

    void Start()
    {
        if (backgroundMusicClip != null)
        {
            StartCoroutine(PlayBackgroundMusic());
        }
        else
        {
            Debug.LogWarning("Background music clip is not assigned!");
        }
    }

    private IEnumerator PlayBackgroundMusic()
    {
        // Play the music once and then wait for the clip's length to loop
        while (true)
        {
            // Play the background music at the main camera's position with specified volume
            AudioSource.PlayClipAtPoint(backgroundMusicClip, Camera.main.transform.position, volume);
            yield return new WaitForSeconds(backgroundMusicClip.length); // Wait for the clip to finish
        }
    }
}
