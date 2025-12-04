using UnityEngine;

public class CutsceneVolume : MonoBehaviour
{

    GameObject Player;

    void Start()
    {
        
    }
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        Player = other.gameObject;
        Player.GetComponent<AirplaneController>().Active = false;
        Debug.Log("CUTSCENE: Player Disabled"); //Ouput the Collision to the console

        Debug.Log("CUTSCENE: Cutscene Start"); //Ouput the Collision to the console
    }
}

//CUTSCENE SYSTEM:

// - Triggered by entering trigger area - DONE
//
// - Disable Player Controls - DONE
//
// - Camera Animations
//
// - Player Animations
//
// - Audio clips with Subtitles