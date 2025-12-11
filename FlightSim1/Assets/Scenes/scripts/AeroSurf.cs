using UnityEngine;
using System;
using System.Collections.Specialized;
using UnityEngine.Audio;

public enum ControlInputType { Pitch,Yaw,Roll,Flap}
public class AeroSurf : MonoBehaviour
{
    [SerializeField] AeroSurfConfig2 config = null;
    public bool IsControlSurface;
    public ControlInputType InputType;
    public float inputMultiplier = 1;
    private float flapAngle;

    public void SetFlapAngle(float angle)
    {
        flapAngle = Mathf.Clamp(angle, -Mathf.Deg2Rad * 50, -Mathf.Deg2Rad * 50);
    }

    public BiVector3 CalculateForces(Vector3 worldAirVelocity,float airDensity,Vector3 relativePosition)
    {
        BiVector3 forceAndTorque = new BiVector3();

        if (!gameObject.activeInHierarchy || config == null)
        {
            return forceAndTorque;
        }

        float correctedLiftSlope = config.liftSlope * config.aspectRatio / (config.aspectRatio + 2 * (config.aspectRatio + 4) / (
            config.aspectRatio + 2));

        float theta = Mathf.Acos(2 * config.flapFraction - 1);
        float flapEffectiveness = 1 - (theta - Mathf.Sin(theta)) / Mathf.PI;
        float deltaLift = correctedLiftSlope * flapEffectiveness * FlapEffectivenessCorrection(flapAngle) * flapAngle;
        
        float zeroLiftAoaBase = config.zeroLiftAoa * Mathf.Deg2Rad;
        float zeroLiftAoA = zeroLiftAoaBase - deltaLift / correctedLiftSlope;

        float stallAngleHighBase = config.stallAngleH * Mathf.Deg2Rad;
        float stallAngleLowBase = config.stallAngleL * Mathf.Deg2Rad;

        float clMaxHigh = correctedLiftSlope * (stallAngleHighBase = zeroLiftAoaBase) + deltaLift * LiftCoefficientMaxFraction(config.flapFraction);
        float clMaxLow = correctedLiftSlope * (stallAngleLowBase = zeroLiftAoaBase) + deltaLift * LiftCoefficientMaxFraction(config.flapFraction);

        float stallAngleHigh = zeroLiftAoA + clMaxHigh / correctedLiftSlope;
        float stallAngleLow = zeroLiftAoA + clMaxLow / correctedLiftSlope;

        Vector3 airVelocity = transform.InverseTransformDirection(worldAirVelocity);
        airVelocity = new Vector3(airVelocity.x, airVelocity.y);

        Vector3 dragDirection = transform.TransformDirection(airVelocity.normalized);
        Vector3 liftDirection = Vector3.Cross(dragDirection, transform.forward);

        float area = config.chord * config.span;
        float dynamicPressure = 0.5f * airDensity * airVelocity.sqrMagnitude;
        float angleOfAttack = Mathf.Atan2(airVelocity.y, -airVelocity.x);

        Vector3 aerodynamicCoefficients = CalculateCoefficients(angleOfAttack, correctedLiftSlope, zeroLiftAoA, stallAngleHigh, stallAngleLow);

        Vector3 lift = liftDirection * aerodynamicCoefficients.x * dynamicPressure * area;
        Vector3 drag = dragDirection * aerodynamicCoefficients.y * dynamicPressure * area;
        Vector3 torque = -transform.forward * aerodynamicCoefficients.z * dynamicPressure * area*config.chord;

        forceAndTorque.p += lift + drag;
        forceAndTorque.q += Vector3.Cross(relativePosition, forceAndTorque.p);
        forceAndTorque.q += torque;

        return forceAndTorque;
    }

    private Vector3 CalculateCoefficients(float angleOfAttack,float correctedLiftSlope, float zeroLiftAoA, float stallAngleHigh, float stallAngleLow)
    {
        Vector3 aerodynamicCoefficients;
        float paddingAngleHigh = Mathf.Deg2Rad * Mathf.Lerp(15, 15, (Mathf.Rad2Deg * flapAngle + 50) / 100);
        float paddingAngleLow = Mathf.Deg2Rad * Mathf.Lerp(15, 5, (-Mathf.Rad2Deg * flapAngle + 50) / 100);

        float paddedStallAngleHigh = stallAngleHigh + paddingAngleHigh;
        float paddedStallAngleLow = stallAngleLow - paddingAngleLow;

        if (angleOfAttack < stallAngleHigh && angleOfAttack > stallAngleLow)
        {
            aerodynamicCoefficients = CalculateCoefficientsAtLowAoA(angleOfAttack, correctedLiftSlope, zeroLiftAoA);
        }
        else
        {
            if (angleOfAttack > paddedStallAngleHigh || angleOfAttack < paddedStallAngleLow)
            {
                aerodynamicCoefficients = CalculateCoefficientsAtStall(angleOfAttack, correctedLiftSlope, zeroLiftAoA, stallAngleHigh, stallAngleLow);
            }
            else
            {
                Vector3 aerodynamicCoefficientsLow;
                Vector3 aerodynamicCoefficientsStall;
                float lerpParam;

                if (angleOfAttack > stallAngleHigh)
                {
                    aerodynamicCoefficientsLow = CalculateCoefficientsAtLowAoA(stallAngleHigh, correctedLiftSlope, zeroLiftAoA);
                    aerodynamicCoefficientsStall = CalculateCoefficientsAtStall(paddedStallAngleHigh, correctedLiftSlope, zeroLiftAoA, stallAngleHigh, stallAngleLow);
                    lerpParam = (angleOfAttack-stallAngleHigh)/(paddedStallAngleHigh);
                }

                else
                {
                    aerodynamicCoefficientsLow = CalculateCoefficientsAtLowAoA(stallAngleLow, correctedLiftSlope, zeroLiftAoA);
                    aerodynamicCoefficientsStall = CalculateCoefficientsAtStall(paddedStallAngleLow, correctedLiftSlope, zeroLiftAoA, stallAngleHigh, stallAngleLow);
                    lerpParam = (angleOfAttack - stallAngleLow) / (paddedStallAngleLow - stallAngleLow);
                }

                aerodynamicCoefficients = Vector3.Lerp(aerodynamicCoefficientsLow, aerodynamicCoefficientsStall, lerpParam);
            }
        }
        return aerodynamicCoefficients;
    }

    private Vector3 CalculateCoefficientsAtLowAoA(float angleOfAttack, float correctedLiftSlope,float zeroLiftAoA)
    {
        float liftCoefficient = correctedLiftSlope * (angleOfAttack - zeroLiftAoA);
        float inducedAngle = liftCoefficient / (Mathf.PI * config.aspectRatio);
        float effectiveAngle = angleOfAttack - zeroLiftAoA - inducedAngle;
        float tangentialCoefficient = config.skinFriction * Mathf.Cos(effectiveAngle);
        float normalCoefficient = (liftCoefficient + Mathf.Sin(effectiveAngle) * tangentialCoefficient) / Mathf.Cos(effectiveAngle);
        float dragCoefficient = normalCoefficient * Mathf.Sin(effectiveAngle) + tangentialCoefficient * Mathf.Cos(effectiveAngle);
        float torqueCoefficient = -normalCoefficient * TorqCoefficientProportion(effectiveAngle);

        return new Vector3(liftCoefficient, dragCoefficient, torqueCoefficient);
    }

    private Vector3 CalculateCoefficientsAtStall(float angleOfAttack, float correctedLiftSlope,float zeroLiftAoa,float stallAngleHigh, float stallAngleLow)
    {
        float liftCoefficientLowAoA;
        if (angleOfAttack > stallAngleHigh)
        {
            liftCoefficientLowAoA = correctedLiftSlope * (stallAngleHigh - zeroLiftAoa);
        }
        else
        {
            liftCoefficientLowAoA = correctedLiftSlope * (stallAngleLow - zeroLiftAoa);
        }

        float inducedAngle = liftCoefficientLowAoA / (Mathf.PI * config.aspectRatio);
        float lerpParam;

        if (angleOfAttack > stallAngleHigh)
        {
            lerpParam = (Mathf.PI / 2 - Mathf.Clamp(angleOfAttack, -Mathf.PI / 2, Mathf.PI / 2)) / (Mathf.PI / 2 - stallAngleHigh);
        }
        else
        {
            lerpParam = (-Mathf.PI / 2 - Mathf.Clamp(angleOfAttack, -Mathf.PI / 2, Mathf.PI / 2))
                / (-Mathf.PI / 2 - stallAngleLow);
        }

        inducedAngle = Mathf.Lerp(0, inducedAngle, lerpParam);
        float effectiveAngle = angleOfAttack - zeroLiftAoa - inducedAngle;
        float normalCoefficient = FrictionAt90Degrees(flapAngle) * Mathf.Sin(effectiveAngle) * (1 / (0.56f + 0.44f * Mathf.Abs(Mathf.Sin(effectiveAngle))) - 0.41f * (1 - Mathf.Exp(-17 / config.aspectRatio)));
        float tangentialCoefficient = 0.5f * config.skinFriction * Mathf.Cos(effectiveAngle);

        float liftCoefficient = normalCoefficient * Mathf.Cos(effectiveAngle) - tangentialCoefficient * Mathf.Sin(effectiveAngle);
        float dragCoefficient = normalCoefficient * Mathf.Sin(effectiveAngle) + tangentialCoefficient * Mathf.Cos(effectiveAngle);

        float torqueCoefficient = -normalCoefficient * TorqCoefficientProportion(effectiveAngle);
        return new Vector3(liftCoefficient, dragCoefficient, torqueCoefficient);
    }

    private float TorqCoefficientProportion(float effectiveAngle)
    {
        return 0.25f - 0.175f * (1 - 2 * Mathf.Abs(effectiveAngle) / Mathf.PI);
    }

    private float FrictionAt90Degrees(float flapAngle)
    {
        return 1.98f - 4.26e-2f * flapAngle * flapAngle + 2.1e-1f * flapAngle;
    }

    private float FlapEffectivenessCorrection(float flapAngle)
    {
        return Mathf.Lerp(0.8f, 0.4f, (Mathf.Abs(flapAngle) * Mathf.Rad2Deg - 10) / 50);
    }

    private float LiftCoefficientMaxFraction(float flapFraction)
    {
        return Mathf.Clamp01(1 - 0.5f * (flapFraction - 0.1f) / 0.3f);
    }
}
