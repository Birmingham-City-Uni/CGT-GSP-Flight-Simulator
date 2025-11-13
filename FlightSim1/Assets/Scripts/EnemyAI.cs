using System;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using Random = UnityEngine.Random;

public enum State { Idle, LockOn, Attack, Retreat }

public class EnemyAI : MonoBehaviour
{
    public Transform target;

    public State currentState = State.Idle;

    [Header("Enemy Settings")]
    public float moveSpeed;
    public float turnSpeed;
    public float attackRange;
    public float retreatDistance;

    [Header("Projectile Settings")]
    public GameObject projectile;
    public float marginOfError = 1.5f;  // The chance of a projectile missing
    private float projectileSpeed = 25;

    private Vector3 targetLastPos;
    private float interval = 5; // Time between attacks
    private float timer;

    void Start()
    {
        if (target == null)
        {
            target = GameObject.FindWithTag("Player").transform;
        }
        targetLastPos = target.position;
        if (projectile == null)
        {
            Debug.LogError("Projectile prefab not found!");
        }
    }

    void Update()
    {
        // Decides what behaviour to perform.
        switch (currentState)
        {
            case State.Idle:
                IdleBehaviour();
                Debug.Log("Currently Idle");
                break;
            case State.LockOn:
                LockOnBehaviour();
                Debug.Log("Locking On!!");
                break;
            case State.Attack:
                AttackBehaviour();
                Debug.DrawLine(transform.position, target.position, Color.blue);
                Debug.Log("Attacking!!");
                break;
            case State.Retreat:
                RetreatBehaviour();
                Debug.Log("Retreating...");
                break;
        }
    }

    private void LateUpdate()
    {
        // Calculates predicted position based on target movement and projectile speed.
        Vector3 aimedPosition = target.position + (Trajectory() * TimeToReach());

        if (timer > interval)
        {
            ResetTimer();
            FireProjectile(aimedPosition);
        }

        timer += Time.deltaTime;
    }
    // Sets the timer to 0 and then generates a new random interval for the next projectile.
    void ResetTimer()
    {
        timer -= interval;
        interval = Random.Range(1f, 4f);
    }

    #region [Calculating projectile aim]
    Vector3 Trajectory()
    {
        // Calculates the trajectory of the target
        Vector3 trajectory = (target.position - targetLastPos) / Time.deltaTime;
        targetLastPos = target.position;

        return trajectory;
    }

    // Calculates how long it would take the projectile to reach the target.
    float TimeToReach()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        return distance / projectileSpeed;
    }
    // Fires the projectile at the targets position.
    void FireProjectile(Vector3 aimedPosition)
    {
        // Instantiate a projectile with the current transform.position and Quaternion.identity
        GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        // Get the projectile's speed
        newProjectile.GetComponent<Projectile>().speed = projectileSpeed;
        // Aim the projectile at the target position + the AimError(marginOfError)
        newProjectile.transform.LookAt(aimedPosition + AimError(marginOfError));
    }
    // Selects a random point in a sphere to aim at - mimics real pilot accuracy.
    Vector3 AimError(float amount)
    {
        return Random.insideUnitSphere * amount;
    }
    #endregion

    #region [State Behaviours]
    // The enemy flies around until the target is close enough.
    void IdleBehaviour()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        if (DistanceToTarget() < attackRange * 2)
        {
            currentState = State.LockOn;
        }
    }
    // The enemy has a lock on the target and constantly moves towards them.
    void LockOnBehaviour()
    {
        MoveTowardsPlayer(target.position);
        // If the enemy gets close enough, they start to attack
        if (DistanceToTarget() < attackRange)
        {
            currentState = State.Attack;
        }
    }
    // The enemy stays on target during the attack state.
    void AttackBehaviour()
    {
        MoveTowardsPlayer(target.position);
        //Debug.DrawLine(transform.position, target.position, Color.red);

        if (DistanceToTarget() < retreatDistance)
        {
            currentState = State.Retreat;
        }
    }
    // If the target is out of range, the enemy retreats.
    void RetreatBehaviour()
    {
        Vector3 awayFromPlayer = transform.position - target.position;
        MoveTowardsPlayer(transform.position + awayFromPlayer);

        if (DistanceToTarget() > attackRange * 1.5f)
        {
            currentState = State.Idle;
        }
    }
    #endregion

    private float DistanceToTarget() => Vector3.Distance(transform.position, target.position);

    // The enemy moves towards and rotates to face the player.
    void MoveTowardsPlayer(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);

        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    // Shows the different ranges that the enemy AI has.
    private void OnDrawGizmos()
    {
        // [Targeting range]
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange * 2);

        // [Shooting range]
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        // [Retreating range]
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, retreatDistance);
    }
}
