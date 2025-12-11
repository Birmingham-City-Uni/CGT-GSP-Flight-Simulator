using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class AirplaneControl : MonoBehaviour
{
   
    [SerializeField] List<AeroSurf> controlSurfaces = null;

    [SerializeField] float rollControlSensitivity = 0.2f;

    [SerializeField] float pitchControlSensitivity = 0.2f;

    [SerializeField] float yawControlSensitivity = 0.2f;

    [Range(-1, 1)] public float pitch;

    [Range(-1, 1)] public float yaw;

    [Range(-1, 1)] public float roll;

    [Range(0, 1)] public float flap;

    float thrustPercent;

    float brakesTorque;

    AircraftPhysics aircraftPhysics;

    Rigidbody rb;

    private void Start()
    {
        aircraftPhysics = GetComponent<AircraftPhysics>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        pitch = Input.GetAxis("Vertical");
        roll = Input.GetAxis("Horizontal");
        yaw = Input.GetAxis("Yaw");
    
        if (Input.GetKeyDown(KeyCode.Space))
        {
            thrustPercent = thrustPercent > 0 ? 0 : 1f;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            flap = flap > 0 ? 0 : 0.3f;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            brakesTorque = brakesTorque > 0 ? 0 : 100f;
        }
    }

    private void FixedUpdate()
    {
        SetControlSurfacesAngle(pitch, roll, yaw, flap);
        aircraftPhysics.SetThrustPercent(thrustPercent);
        //foreach (var wheel in wheels)
        //{
        //    wheel.brakesTorque;
        //    wheel.motorTorque = 0.1f;
        //}
    }

    public void SetControlSurfacesAngle(float pitch,float roll, float yaw, float flap)
    {
        foreach (var surface in controlSurfaces)
        {
            if (surface == null || !surface.IsControlSurface) continue;
            switch (surface.InputType)
            {
                case ControlInputType.Pitch:
                    surface.SetFlapAngle(pitch * pitchControlSensitivity * surface.inputMultiplier);
                    break;
                case ControlInputType.Roll:
                    surface.SetFlapAngle(roll*rollControlSensitivity*surface.inputMultiplier); 
                    break;
                case ControlInputType.Yaw:
                    surface.SetFlapAngle(yaw * yawControlSensitivity * surface.inputMultiplier);
                    break;
                case ControlInputType.Flap:
                    surface.SetFlapAngle(flap * surface.inputMultiplier); 
                    break;
            }
        }
    }



}
