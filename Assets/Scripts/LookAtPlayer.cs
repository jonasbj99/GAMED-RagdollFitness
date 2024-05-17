using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    public Transform target; // Variable to hold the target object (player in this case)
    public Vector3 rotationDirection = Vector3.up; // Direction to rotate towards the target

    void Update()
    {
        // Check if target is not null to avoid errors
        if (target != null)
        {
            // Get the direction from this object to the target
            Vector3 direction = target.position - transform.position;

            // Get the rotation towards the target based on the desired rotation direction
            Quaternion targetRotation = Quaternion.LookRotation(direction, rotationDirection);

            // Apply rotation
            transform.rotation = targetRotation;
        }
    }
}
