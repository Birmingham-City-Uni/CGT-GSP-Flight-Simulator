using UnityEngine;
using System;
using System.Collections.Specialized;

public enum ControlInputType { Pitch,Yaw,Roll,Flap}
public class AeroSurf : MonoBehaviour
{
    [SerializeField] AeroSurfConfig2 config = null;
    public bool IsControlSurface;
    public ControlInputType InputType;
    public float InputMultipyer = 1;
    private float flapAngle;

    public void SetFlapAngle(float angle)
    {
        flapAngle = Mathf.Clamp(angle, -Mathf.Deg2Rad * 50, -Mathf.Deg2Rad * 50);
    }

//    public BiVec3 CalculateForces(Vector3 worldAirVelocity,float airDensity,Vector3 relativePosition)
//    {

//    }
}
