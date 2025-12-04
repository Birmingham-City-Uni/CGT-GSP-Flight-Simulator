using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-----------Audio Source-----------")]
    // Attach the music source to this
    [SerializeField] AudioSource musicSource;
    // Attach the sound effects to this
    [SerializeField] AudioSource SFXSource;

    [Header("-----------Clips Source-----------")]
    public AudioClip backgroundMusic;   // [EXAMPLE]
    public AudioClip crash;             // [EXAMPLE]
    public AudioClip engineStage1;
    public AudioClip engineStage2;
    public AudioClip engineStage3;

    private void Start()
    {
        // [EXAMPLE]
        // Select the audio clip
        musicSource.clip = backgroundMusic;
        // Background music would go here as it plays from beginning
        musicSource.Play();      // Plays the music
    }

    void Update()
    {
        // [EXAMPLE]
        if (true)
        {
            // Select an audio clip for the audio source
            SFXSource.clip = crash;
            // Play the clip
            SFXSource.PlayOneShot(crash, 0.8f);     // PlayOneShot(clip, volume) plays the sound without interrupting the other sounds going on
        }

        // Play different engine sounds depending on speed
        //if (player.velocity <= slow) { SFXSource.loop = true, SFXSource.Play(engineStage1) }
        //if (player.velocity > slow && velocity < fast) { SFXSource.loop = true, SFXSource.Play(engineStage2) }
        //if (player.velocity > fast) { SFXSource.loop = true, SFXSource.Play(engineStage3) }
    }
}
