using UnityEngine;

public class BellTouchDetection : MonoBehaviour
{
    [SerializeField] AudioSource strengthAudio;

    public float scoreIncrementInterval = 5f; // Interval to increment the score
    private float timer = 0f; // Timer to track the time

    private void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // If the timer exceeds the increment interval
        if (timer >= scoreIncrementInterval)
        {
            // Reset the timer
            timer = 0f;

            // Check if the hand is touching an object with the tag "Bell"
            Collider[] colliders = Physics.OverlapSphere(transform.position, 0.1f); // Adjust the radius as needed
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Bell"))
                {
                    // Increment the score
                    StrengthBar.currentStrength+=2;
                    Debug.Log("Score incremented: " + StrengthBar.currentStrength);
                    strengthAudio.Play();
                    break; // Exit the loop if a bell is found
                }
            }
        }
    }
}
