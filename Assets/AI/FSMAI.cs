using UnityEngine;
using UnityEngine.AI;

public class FSMAI : MonoBehaviour
{
    public float detectionRange = 10f;
    public float fieldOfViewAngle = 110f;
    public Transform player;
    public float patrolRadius = 20f;
    public float patrolInterval = 5f;
    public float stopDistance = 1.5f;
    public float bumpDistance = 2f;

    private NavMeshAgent agent;
    private bool playerInSight;
    private Vector3 lastKnownPlayerPosition;
    private float patrolTimer;
    private bool hasCollidedWithPlayer;

    private enum State { Patrolling, Chasing, Returning }
    private State currentState;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = State.Patrolling;
        SetRandomDestination();
    }

    void Update()
    {
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
        DetectPlayer();
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
        }
    }

    void Chase()
    {
        Debug.Log("State: Chasing");
        if (playerInSight)
        {
            agent.SetDestination(player.position);
            lastKnownPlayerPosition = player.position;

            if (Vector3.Distance(transform.position, player.position) <= stopDistance)
            {
                hasCollidedWithPlayer = true;
                currentState = State.Returning;
            }
        }
        else
        {
            agent.SetDestination(lastKnownPlayerPosition);
            if (agent.remainingDistance < agent.stoppingDistance)
            {
                currentState = State.Returning;
            }
        }

        // Check for collision with player
        if (Vector3.Distance(transform.position, player.position) <= bumpDistance)
        {
            hasCollidedWithPlayer = true;
            currentState = State.Returning;
        }
    }

    void ReturnToPatrol()
    {
        Debug.Log("State: Returning");
        if (hasCollidedWithPlayer)
        {
            SetRandomDestination();
            hasCollidedWithPlayer = false;
            currentState = State.Patrolling;
        }
    }

    void SetRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, 1))
        {
            Vector3 finalPosition = hit.position;
            agent.SetDestination(finalPosition);
        }
    }

    void DetectPlayer()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float angle = Vector3.Angle(directionToPlayer, transform.forward);

        if (directionToPlayer.magnitude < detectionRange && angle < fieldOfViewAngle * 0.5f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + transform.up, directionToPlayer.normalized, out hit, detectionRange))
            {
                Debug.DrawRay(transform.position + transform.up, directionToPlayer.normalized * detectionRange, Color.green); // Draw ray
                if (hit.collider.gameObject == player.gameObject)
                {
                    Debug.Log("Player detected");
                    playerInSight = true;
                    return;
                }
            }
            else
            {
                Debug.DrawRay(transform.position + transform.up, directionToPlayer.normalized * detectionRange, Color.red); // Draw ray
            }
        }
        Debug.Log("Player not detected");
        playerInSight = false;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player.gameObject && currentState == State.Chasing)
        {
            // Handle the collision with the player
            hasCollidedWithPlayer = true;
            currentState = State.Returning;
        }
    }

    void OnDrawGizmos()
    {
        if (agent != null)
        {
            // Draw the path of the NavMeshAgent
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, agent.destination);
        }

        // Draw the detection range
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Draw the field of view
        Vector3 leftBoundary = Quaternion.Euler(0, -fieldOfViewAngle * 0.5f, 0) * transform.forward * detectionRange;
        Vector3 rightBoundary = Quaternion.Euler(0, fieldOfViewAngle * 0.5f, 0) * transform.forward * detectionRange;
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, leftBoundary);
        Gizmos.DrawRay(transform.position, rightBoundary);
    }
}
