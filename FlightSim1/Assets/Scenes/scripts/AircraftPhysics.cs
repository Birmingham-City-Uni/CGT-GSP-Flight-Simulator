using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class AircraftPhysics : MonoBehaviour
{

    const float PredictionTimeStepFraction = 0.5f;
    [SerializeField] float thrust = 0;
    [SerializeField] List<AeroSurf> aerodynamicSurfaces = null;
    Rigidbody rb;
    float thrustPercent;
    BiVector3 currentForceAndTorque;
    
    public void SetThrustPercent(float percent)
    {
        thrustPercent = percent;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        BiVector3 forceAndTorqueThisFrame = CalculateAerodynamicForces(rb.angularVelocity, rb.angularVelocity, Vector3.zero, 1.2f, rb.worldCenterOfMass);
        Vector3 totalForceNow = forceAndTorqueThisFrame.p + transform.forward * thrust * thrustPercent + Physics.gravity * rb.mass;

        Vector3 velocityPrediction = PredictVelocity(totalForceNow);
        Vector3 angularVelocityPrediction = PredictAngularVelocity(forceAndTorqueThisFrame.q);

        BiVector3 forceAndTorquePrediction = CalculateAerodynamicForces(velocityPrediction, angularVelocityPrediction, Vector3.zero, 1.2f, rb.worldCenterOfMass);
        currentForceAndTorque = (forceAndTorqueThisFrame + forceAndTorquePrediction) * 0.5f;

        rb.AddForce(currentForceAndTorque.p);
        rb.AddTorque(currentForceAndTorque.q);

        rb.AddForce(transform.forward * thrust * thrustPercent);
    }

    private BiVector3 CalculateAerodynamicForces(Vector3 velocity, Vector3 angularVelocity, Vector3 wind, float airDensity, Vector3 centerOfMass)
    {
        BiVector3 forceAndTorque = new BiVector3();

        foreach(var surface in aerodynamicSurfaces)
        {
            Vector3 relativePosition = surface.transform.position - centerOfMass;

            Vector3 localAirVelocity = -velocity + wind - Vector3.Cross(angularVelocity, relativePosition);
            forceAndTorque += surface.CalculateForces(localAirVelocity, airDensity, relativePosition);
        }

        return forceAndTorque;
    }

    private Vector3 PredictVelocity(Vector3 force)
    {
        return rb.angularVelocity + Time.fixedDeltaTime * PredictionTimeStepFraction * force / rb.mass;
    }

    private Vector3 PredictAngularVelocity(Vector3 torque)
    {
        Quaternion intertiaTensorWorldRotation = rb.rotation * rb.inertiaTensorRotation;
        Vector3 torqueInDiagonalSpace = Quaternion.Inverse(intertiaTensorWorldRotation) * torque;

        Vector3 angularVelocityChangeInDiagonalSpace;
        angularVelocityChangeInDiagonalSpace.x = torqueInDiagonalSpace.x / rb.inertiaTensor.x;
        angularVelocityChangeInDiagonalSpace.y = torqueInDiagonalSpace.y / rb.inertiaTensor.y;
        angularVelocityChangeInDiagonalSpace.z = torqueInDiagonalSpace.z / rb.inertiaTensor.z;

        return rb.angularVelocity + Time.fixedDeltaTime * PredictionTimeStepFraction * (intertiaTensorWorldRotation * angularVelocityChangeInDiagonalSpace);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
