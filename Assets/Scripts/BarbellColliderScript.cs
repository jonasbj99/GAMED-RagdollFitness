using UnityEngine;

public class BarbellColliderScript : MonoBehaviour
{
    [SerializeField] AudioSource strengthAudio;

    public Transform waypoint1;
    public Transform waypoint2;

    public GameObject waypoint1object;
    public GameObject waypoint2object;

    private int score = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Barbell"))
        {
            if (other.transform.position == waypoint1.position)
            {
                Debug.Log("Waypoint 1 reached!");
                StrengthBar.currentStrength+=3;
                waypoint1object.SetActive(false);
                waypoint2object.SetActive(true);
                strengthAudio.Play();
            }
            else if (other.transform.position == waypoint2.position)
            {
                Debug.Log("Waypoint 2 reached!");
                StrengthBar.currentStrength+=3;
                waypoint2object.SetActive(false);
                waypoint1object.SetActive(true);
                strengthAudio.Play();
            }

            Debug.Log("Score: " + score);
        }
    }
}
