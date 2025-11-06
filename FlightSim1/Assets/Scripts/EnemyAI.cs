using UnityEngine;

public enum State { Idle, LockOn, Attack, Retreat }

public class EnemyAI : MonoBehaviour
{
    public GameObject player;

    public State currentState = State.Idle;

    [Header("Flight Settings")]
    public float moveSpeed;
    public float turnSpeed;
    public float attackDistance;
    public float retreatDistance;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
    }

    void Update()
    {
        // Decides what behaviour to perform.
        switch (currentState)
        {
            case State.Idle:
                IdleBehaviour();
                print("Currently Idle");
                break;
            case State.LockOn:
                LockOnBehaviour();
                print("Locking On!!");
                break;
            case State.Attack:
                AttackBehaviour();
                print("Attacking!!");
                break;
            case State.Retreat:
                RetreatBehaviour();
                print("Retreating...");
                break;
        }
    }

    // The enemy flies around until the player is close enough.
    void IdleBehaviour()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < attackDistance * 2)
        {
            currentState = State.LockOn;
        }
    }
    // The enemy has a lock on the player and constantly moves towards them.
    void LockOnBehaviour()
    {
        MoveTowardsPlayer(player.transform.position);
        // If the enemy gets close enough, they start to attack
        if (Vector3.Distance(transform.position, player.transform.position) < attackDistance)
        {
            currentState = State.Attack;
        }
    }
    // The enemy stays on target during the attack state.
    void AttackBehaviour()
    {
        MoveTowardsPlayer(player.transform.position);
        Debug.DrawLine(transform.position, player.transform.position, Color.red);

        if (Vector3.Distance(transform.position, player.transform.position) > retreatDistance)
        {
            currentState = State.Retreat;
        }
    }
    // If the player is out of range, the enemy retreats.
    void RetreatBehaviour()
    {
        Vector3 awayFromPlayer = transform.position - player.transform.position;
        MoveTowardsPlayer(transform.position + awayFromPlayer);

        if (Vector3.Distance(transform.position, player.transform.position) > retreatDistance)
        {
            currentState = State.Idle;
        }
    }

    // The enemy moves towards and rotates to face the player.
    void MoveTowardsPlayer(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);

        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}
