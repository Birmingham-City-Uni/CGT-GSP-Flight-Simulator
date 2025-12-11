using NUnit.Framework.Constraints;
using System;
using UnityEngine;

public enum EngineState { LOW, MID, HIGH, HIGH_VTOL };

public class AudioManager : MonoBehaviour
{
    [Header("-----------Audio Source-----------")]
    // Attach the music source to this
    [SerializeField] AudioSource musicSource;
    // Attach the sound effects to this
    [SerializeField] AudioSource SFXSource;

    [Header("-----------Clips Source-----------")]
    //public AudioClip backgroundMusic;   // [EXAMPLE]
    //public AudioClip crash;             // [EXAMPLE]
    public AudioClip lowThrottle;
    public AudioClip midThrottle;
    public AudioClip highThrottle;
    public AudioClip highThrottleVTOL;

    [Header("-----------Player & Enemy references-----------")]
    public EnemyAI enemy;
    private EngineState currentState = EngineState.LOW;

    private void Start()
    {
        if (enemy == null)
        {
            enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyAI>();
        }
    }

    void Update()
    {
        switch (currentState)
        {
            case EngineState.LOW:
                LowEngine();
                break;
            case EngineState.MID:
                MidEngine();
                break;
            case EngineState.HIGH:
                HighEngine();
                break;
            case EngineState.HIGH_VTOL:
                HighVTOLEngine();
                break;
        }

        //Debug.Log("Playing sound: " + SFXSource.clip);
        //Debug.Log("[CURRENT ENGINE STATE: " + currentState + "]");
    }

    // Plays the low throttle sound
    void LowEngine()
    {
        if (SFXSource.clip != lowThrottle)
        {
            SFXSource.clip = lowThrottle;
            SFXSource.loop = true;
            SFXSource.Play();
        }

        // The mid range speed
        if (enemy.moveSpeed > 4)
        {
            currentState = EngineState.MID;
        }
    }
    // Plays the mid throttle sound
    void MidEngine()
    {
        if (SFXSource.clip != midThrottle)
        {
            SFXSource.clip = midThrottle;
            SFXSource.loop = true;
            SFXSource.Play();
        }

        // The high range speed
        if (enemy.moveSpeed > 5)
        {
            currentState = EngineState.HIGH;
        }
        // The low range speed
        if (enemy.moveSpeed < 5)
        {
            currentState = EngineState.LOW;
        }
    }
    // Plays the high throttle sound (for regular planes)
    void HighEngine()
    {
        if (SFXSource.clip != highThrottle)
        {
            SFXSource.clip = highThrottle;
            SFXSource.loop = true;
            SFXSource.Play();
        }

        // The mid range speed
        if (enemy.moveSpeed < 6)
        {
            currentState = EngineState.MID;
        }
    }
    // Plays the high throttle sound (for VTOL planes)
    void HighVTOLEngine()
    {
        if (SFXSource.clip != highThrottleVTOL)
        {
            SFXSource.clip = highThrottleVTOL;
            SFXSource.loop = true;
            SFXSource.Play();
        }

        // The mid range speed
        if (enemy.moveSpeed < 6)
        {
            currentState = EngineState.MID;
        }
    }
}
