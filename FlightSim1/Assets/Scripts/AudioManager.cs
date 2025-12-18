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

    [Header("-----------Harrier Sound Clips-----------")]
    public AudioClip lowThrottle;
    public AudioClip midThrottle;
    public AudioClip highThrottle;
    public AudioClip highThrottleVTOL;

    [Header("-----------Phantom Sound Clips-----------")]
    public AudioClip acceleration;
    public AudioClip takeOff;
    public AudioClip taxi;

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
        Debug.Log("[CURRENT ENGINE STATE: " + currentState + "]");
    }

    // Plays the low throttle sound - [speed of 4 or slower]
    void LowEngine()
    {
        if (SFXSource.clip != lowThrottle)
        {
            SFXSource.clip = lowThrottle;
            SFXSource.loop = true;
            SFXSource.Play();
        }

        // Go up to mid throttle sound
        if (enemy.moveSpeed >= 5 && enemy.moveSpeed < 8)
        {
            currentState = EngineState.MID;
        }
    }
    // Plays the mid throttle sound - [speed of 5 - 7]
    void MidEngine()
    {
        if (SFXSource.clip != midThrottle)
        {
            SFXSource.clip = midThrottle;
            SFXSource.loop = true;
            SFXSource.Play();
        }

        // Go down to low throttle sound
        if (enemy.moveSpeed > 0 && enemy.moveSpeed <= 4)
        {
            currentState = EngineState.LOW;
        }
        // Go up to high throttle sound
        if (enemy.moveSpeed >= 8)
        {
            currentState = EngineState.HIGH;
        }
    }
    // Plays the high throttle sound (for regular planes) - [speed of 8 or higher]
    void HighEngine()
    {
        if (SFXSource.clip != highThrottle)
        {
            SFXSource.clip = highThrottle;
            SFXSource.loop = true;
            SFXSource.Play();
        }

        // Go down to mid throttle sound
        if (enemy.moveSpeed >= 5 && enemy.moveSpeed <= 7)
        {
            currentState = EngineState.MID;
        }
    }
    // Plays the high throttle sound (for VTOL planes) - [speed of 8 or higher]
    void HighVTOLEngine()
    {
        if (SFXSource.clip != highThrottleVTOL)
        {
            SFXSource.clip = highThrottleVTOL;
            SFXSource.loop = true;
            SFXSource.Play();
        }

        // Go down to mid throttle sound
        if (enemy.moveSpeed >= 5 && enemy.moveSpeed <= 7)
        {
            currentState = EngineState.MID;
        }
    }
}
