using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "AeroSurfConfig2", menuName = "Scriptable Objects/AeroSurfConfig2")]
public class AeroSurfConfig2 : ScriptableObject
{
    public float liftSlope = 6.28f;
    public float skinFriction = 0.02f;
    public float zeroLiftAoa = 0;
    public float stallAngleH = 15;
    public float stallAngleL = -15;
    public float chord = 1;
    public float flapFraction = 0;
    public float span = 1;
    public bool autoAspectRatio = true;
    public float aspectRatio = 2;

    private void OnValidate()
    {
        if (flapFraction > 0.4f)
        {
            flapFraction = 0.4f;
        }
        if (flapFraction < 0)
        {
            flapFraction = 0;
        }

        if (stallAngleH < 0) { 
            stallAngleH = 0;
        }

        if (stallAngleL > 0)
        {
            stallAngleL = 0;
        }

        if (chord < 1e-3f)
        {
            chord = 1e-3f;
        }

        if (autoAspectRatio)
        {
            aspectRatio = span / chord;
        }
    }
}
