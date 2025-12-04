using JetBrains.Annotations;
using System;
using Unity.Hierarchy;
using UnityEngine;

public class AirplaneController : MonoBehaviour
{


    [Header("Flight Settings")]
    public float Speed = 5; // Movement Speed Vaiable

    private float Yaw; // Yaw = Rotation of Y axis
    private float Pitch; // Pitch = Rotation of X axis
    private float Roll; // Roll = Rotation of Z axis

    public float YawAmount = 120; //Speed of Yaw Rotation
    public float PitchAmount = 20; //Speed of Pitch Rotation
    public float RollAmount = 30; //Speed of Roll Rotation

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    { //https://www.youtube.com/watch?v=wq5YYldv1gw
        transform.position += transform.forward * Speed * Time.deltaTime; // Automatically move forward to amount of Speed (+ DeltaTime)

        float Horizontalnput = Input.GetAxis("Horizontal"); // Get Horzontial inputs (A, D, Left Arrow, Right Arrow, Left & Right on Left Analog Stick)
        float VerticalInput = Input.GetAxis("Vertical"); // Get Vertical inputs (W, S, Up Arrow, Down Arrow, Up & Down on Left Analog Stick)

        Yaw += Horizontalnput * YawAmount * Time.deltaTime; // apply YawAmount to Yaw in amount relative to horizontial input (+ DeltaTime)
        Pitch = Mathf.Lerp(0, PitchAmount, Mathf.Abs(VerticalInput)) * Mathf.Sign(VerticalInput); // apply PitchAmount to Pitch in amount relative to vertical input (+ DeltaTime)
        Roll = Mathf.Lerp(0, RollAmount, Mathf.Abs(Horizontalnput)) * -Mathf.Sign(Horizontalnput); // apply RollAmount to Roll in amount relative to horizontial input (+ DeltaTime)

        transform.localRotation = Quaternion.Euler(Vector3.up * Yaw + Vector3.right * Pitch + Vector3.forward * Roll); //Apply all rotations to player

        if (Input.GetButton("Fire1"))
        {
            Debug.Log("FIRE1");
        }

    }


    }
