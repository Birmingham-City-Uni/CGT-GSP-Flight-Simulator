using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Controller : MonoBehaviour
{
    int test = 0;
    int Mins = 0;
    int Seconds = 0;
    int CurrentTime = 0;
    int VelocityOutput;
    int healthOutput = 100;
    Image ThrottleBarImage;
    Image HealthBarImage;
    TMP_Text AmmoText;
    TMP_Text LevelTimeText;
    TMP_Text AltitudeLabel;
    TMP_Text SpeedLabel;
    Vector3 ThrottleScale;
    void Start()
    {
        ThrottleBarImage = GameObject.FindGameObjectWithTag("ThrottleSlider").GetComponent<Image>();
        HealthBarImage = GameObject.FindGameObjectWithTag("HealthSlider").GetComponent<Image>();
        AmmoText = GameObject.FindGameObjectWithTag("AmmoCounter").GetComponent<TMP_Text>();
        LevelTimeText = GameObject.FindGameObjectWithTag("LevelTimeLabel").GetComponent<TMP_Text>();
        AltitudeLabel = GameObject.FindGameObjectWithTag("AltitudeLabel").GetComponent<TMP_Text>();
        SpeedLabel = GameObject.FindGameObjectWithTag("SpeedLabel").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLevelTimeText();
    }

    void UpdateThrottleSlider(float throttle)
    {
        ThrottleScale = ThrottleBarImage.transform.localScale;
        if (throttle < 0) // prevent negative scale
        {
            ThrottleScale.y = 0;
        }
        else
        {
            ThrottleScale.y = throttle;
        }
        ThrottleBarImage.transform.localScale = ThrottleScale;
    }

    void UpdateAmmoText(int ammo)
    {
        AmmoText.text = "Ammo: " + ammo.ToString();
    }

    void UpdateLevelTimeText()
    {
        CurrentTime = (int)Time.time;
        Mins = CurrentTime / 60;
        Seconds = CurrentTime % 60;
        if (Seconds < 10)
        {
            LevelTimeText.text = Mins.ToString() + ":" + "0" + Seconds.ToString();
        }
        else
        {
            LevelTimeText.text = Mins.ToString() + ":" + Seconds.ToString();
        }
    }

    void UpdateAltitudeLabel(int altitude)
    {
        AltitudeLabel.text = "Altitude: " + altitude.ToString();
    }

    void UpdateSpeedLabel(Vector3 velocity)
    {
        VelocityOutput = (int)(Mathf.Sqrt(Mathf.Pow(velocity.x, 2) + Mathf.Pow(velocity.y, 2) + Mathf.Pow(velocity.z, 2))); // calculate the length of the velocity vector
        SpeedLabel.text = "Speed: " + VelocityOutput.ToString();
    }

    /// <summary>
    /// Pass negative value for health loss and positive for health gain
    /// </summary>
    /// <param name="HealthChange"></param>
    void UpdateHealth(int HealthChange)
    {
        healthOutput += HealthChange;
        Vector3 scale = HealthBarImage.transform.localScale;
        scale.y = healthOutput / 100;
        HealthBarImage.transform.localScale = scale;
    }
}
