using UnityEngine;

public class SledColliderScript : MonoBehaviour
{
    public Transform waypoint1;
    public Transform waypoint2;

    public GameObject waypoint1object;
    public GameObject waypoint2object;

    private int score = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sled"))
        {
            if (other.transform.position == waypoint1.position)
            {
                Debug.Log("Waypoint 1 reached!");
                score++;
                waypoint1object.SetActive(true);
                waypoint2object.SetActive(false);
            }
            else if (other.transform.position == waypoint2.position)
            {
                Debug.Log("Waypoint 2 reached!");
                score++;
                waypoint2object.SetActive(true);
                waypoint1object.SetActive(false);
            }

            Debug.Log("Score: " + score);
        }
    }
}
