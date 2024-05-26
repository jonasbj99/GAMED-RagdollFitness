using UnityEngine;

public class PlayerLauncher : MonoBehaviour
{
    public float launchForce = 10f; // The force to launch the player upwards
    public AudioClip launchSound; // Sound to play when launched

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("AI"))
        {
            Rigidbody playerRigidbody = GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                // Apply the launch force upwards
                playerRigidbody.AddForce(Vector3.up * launchForce, ForceMode.Impulse);
                Debug.Log("Launched!");

                // Play the launch sound
                if (launchSound != null)
                {
                    AudioSource.PlayClipAtPoint(launchSound, transform.position);
                }
            }
        }
    }
}
