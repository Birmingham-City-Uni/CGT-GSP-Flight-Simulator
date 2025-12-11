using UnityEditor.UI;
using UnityEngine;

public class CutsceneVolume : MonoBehaviour
{

    GameObject Player;

    [Header("Animation to play:")]
    public GameObject Cutscene;

    [Header("Animation Length (in seconds)")]
    public float CutsceneDuration;


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
                Cutscene = GameObject.FindWithTag("Cutscene");
                Object.Destroy(Cutscene);
                Object.Destroy(this.gameObject);
                Player.GetComponent<AirplaneController>().Active = true;
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