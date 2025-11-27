using UnityEngine;

public class ThrottlePrototype : MonoBehaviour
{
    float throttle = 50;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(throttle);
        if (throttle < 100)
        {
            if (Input.GetKey(KeyCode.W))
            {
                throttle +=.05f;
            }
        }

        if (throttle > 0)
        {
            if (Input.GetKey(KeyCode.S))
            {
                throttle -= .05f;
            }
        }

        if (throttle < 0)
        {
            throttle = 0;
        }

        if (throttle > 100)
        {
            throttle = 100;
        }
    }
}
