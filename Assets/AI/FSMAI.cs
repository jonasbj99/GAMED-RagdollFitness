using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class FSMAI : MonoBehaviour
{
    public float detectionRange = 10f;
    public float fieldOfViewAngle = 110f;
    public Transform player; // Ensure this is correctly assigned in the Inspector
    public float patrolRadius = 20f;
    public float patrolInterval = 5f;
    public float stopDistance = 1.5f;
    public float bumpDistance = 2f;
    public float maxChaseDistance = 15f; // Maximum distance AI can be from last known player position while chasing

    private NavMeshAgent agent;
    private bool playerInSight;
    private Vector3 lastKnownPlayerPosition;
    private float patrolTimer;
    private bool hasCollidedWithPlayer;
    private Quaternion initialRotation; // Store the initial rotation of the AI

    private enum State { Patrolling, Chasing, Returning }
    private State currentState;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = State.Patrolling;
        SetRandomDestination();

        // Store the initial rotation of the AI
        initialRotation = transform.rotation;
    }

    void Update()
    {
        DetectPlayer(); // Detect player first

        switch (currentState)
        {
            case State.Patrolling:
                Patrol();
                break;
            case State.Chasing:
                Chase();
                break;
            case State.Returning:
                ReturnToPatrol();
                break;
        }
    }

    void Patrol()
    {
        Debug.Log("State: Patrolling");
        patrolTimer += Time.deltaTime;

        if (patrolTimer >= patrolInterval || agent.remainingDistance < agent.stoppingDistance)
        {
            SetRandomDestination();
            patrolTimer = 0f;
        }

        if (playerInSight)
        {
            currentState = State.Chasing;
            Debug.Log("Transition to Chasing");
        }
    }

    void Chase()
    {
        Debug.Log("State: Chasing");

        if (playerInSight)
        {
            SetDestination(player.position);
            lastKnownPlayerPosition = player.position;

            if (Vector3.Distance(transform.position, player.position) <= stopDistance)
            {
                hasCollidedWithPlayer = true;
                currentState = State.Returning;
                Debug.Log("Transition to Returning due to collision");
            }
        }
        else
        {
            SetDestination(lastKnownPlayerPosition);

            if (agent.remainingDistance < agent.stoppingDistance)
            {
                currentState = State.Returning;
                Debug.Log("Transition to Returning due to lost sight of player");
            }

            // Check if the AI is too far from the last known player position
            if (Vector3.Distance(transform.position, lastKnownPlayerPosition) > maxChaseDistance)
            {
                Debug.Log("AI pushed too far, returning to patrol");
                currentState = State.Returning;
            }
        }

        // Check if the AI has collided with the player
        if (hasCollidedWithPlayer)
        {
            // Check if the player is still within detection range
            if (!playerInSight)
            {
                currentState = State.Returning;
                Debug.Log("Transition to Returning because player is out of range");
            }
        }
        else
        {
            // Check if the player is out of range
            if (!playerInSight)
            {
                currentState = State.Returning;
                Debug.Log("Transition to Returning because player is out of range");
            }
        }

        if (Vector3.Distance(transform.position, player.position) <= bumpDistance)
        {
            hasCollidedWithPlayer = true;
            currentState = State.Returning;
            Debug.Log("Transition to Returning due to bump distance");
        }
    }


    void ReturnToPatrol()
    {
        Debug.Log("State: Returning");

        // Stop the agent to ensure it doesn't slide
        agent.isStopped = true;
        agent.ResetPath();

        // Reset the rotation of the AI to its initial rotation
        transform.rotation = initialRotation;

        // Check if the agent is fully stopped before proceeding
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            // After stopping, set a new patrol destination and transition back to Patrolling
            SetRandomDestination();
            currentState = State.Patrolling;
            hasCollidedWithPlayer = false;
            Debug.Log("Transition to Patrolling");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check for collision with player and transition to returning state
        if (collision.gameObject.CompareTag("Player") && currentState == State.Chasing)
        {
            hasCollidedWithPlayer = true;
            currentState = State.Returning;
            Debug.Log("Collision with player, transition to Returning");
        }
    }

    void SetRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, NavMesh.AllAreas))
        {
            Vector3 finalPosition = hit.position;

            // Ensure the destination is not too close to the edge of the NavMesh
            if (!IsInsideNavMesh(finalPosition))
            {
                Debug.Log("Random destination too close to the edge of the NavMesh, setting a new destination");
                SetRandomDestination();
                return;
            }

            SetDestination(finalPosition);
        }
    }

    bool IsInsideNavMesh(Vector3 position)
    {
        NavMeshHit hit;
        return NavMesh.SamplePosition(position, out hit, 0.1f, NavMesh.AllAreas);
    }

    void SetDestination(Vector3 target)
    {
        // Only set a new destination if it's significantly different from the current one
        if (Vector3.Distance(agent.destination, target) > 1f)
        {
            agent.isStopped = false;
            agent.SetDestination(target);
            Debug.Log($"New Destination Set: {target}");
        }
    }

    void DetectPlayer()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;
        float angle = Vector3.Angle(directionToPlayer, transform.forward);

        if (distanceToPlayer < detectionRange && angle < fieldOfViewAngle * 0.5f)
        {
            Debug.Log("Player detected");
            playerInSight = true;
        }
        else
        {
            Debug.Log("Player not detected");
            playerInSight = false;
        }
    }

    void OnDrawGizmos()
    {
        if (agent != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, agent.destination);
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Vector3 leftBoundary = Quaternion.Euler(0, -fieldOfViewAngle * 0.5f, 0) * transform.forward * detectionRange;
        Vector3 rightBoundary = Quaternion.Euler(0, fieldOfViewAngle * 0.5f, 0) * transform.forward * detectionRange;
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, leftBoundary);
        Gizmos.DrawRay(transform.position, rightBoundary);
    }
}
