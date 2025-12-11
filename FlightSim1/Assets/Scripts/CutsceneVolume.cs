using UnityEditor.UI;
using UnityEngine;

public class CutsceneVolume : MonoBehaviour
{

    GameObject Player;

    [Header("Animation to play:")]
    public GameObject Cutscene;

    [Header("Animation Length (in seconds)")]
    public float CutsceneDuration;

    [Header("End Level after cutscene")]
    public bool LevelEnd;
    public GameObject LevelCompleteUI;

    [Header("DEBUG")]
    public float CurrentFrame = 0.0f;

    public bool CutsceneActive = false;

    void Start()
    {
       // Debug.Log("deltatime = " + Time.deltaTime); //Ouput the Collision to the console
    }
    void Update()
    {
        if (CutsceneActive == true)
        { 
            CurrentFrame += Time.deltaTime;

            if (CurrentFrame >= CutsceneDuration)
            {
                Debug.Log("CUTSCENE: Cutscene End"); //Ouput the Collision to the console
                Cutscene = GameObject.FindWithTag("Cutscene");
                Object.Destroy(Cutscene);

                switch(LevelEnd)
                {
                    case true:
                        Instantiate(LevelCompleteUI);
                        Debug.Log("CUTSCENE: Level Completed UI Spawned"); //Ouput the Collision to the console
                        Object.Destroy(this.gameObject);
                        break;

                    case false:
                        Player.GetComponent<AirplaneController>().Active = true;
                        Debug.Log("CUTSCENE: Player Enabled"); //Ouput the Collision to the console
                        Object.Destroy(this.gameObject);
                        break;

                }
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        Player = other.gameObject;
        Player.GetComponent<AirplaneController>().Active = false;
        Debug.Log("CUTSCENE: Player Disabled"); //Ouput the Collision to the console

        Debug.Log("CUTSCENE: Cutscene Start"); //Ouput the Collision to the console

        Instantiate(Cutscene);

        CutsceneActive = true;
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