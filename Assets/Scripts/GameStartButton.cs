using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public GameObject cameraWithCanvasPrefab; // Assign your camera with canvas prefab in the Inspector
    public GameObject player; // Assign your player prefab in the Inspector
    public GameObject playerExtra; // Assign your player extra prefab in the Inspector

    // Function called when the button is clicked
    public void OnButtonClick()
    {
        // Get reference to the camera with canvas
        GameObject cameraWithCanvas = GameObject.FindGameObjectWithTag("MainCamera");

        // Delay the deletion by 2 seconds
        Invoke("DeleteCameraWithCanvas", 2f);

        // Spawn a new player prefab
        player.SetActive(true);
        // Spawn a new player extra prefab
        playerExtra.SetActive(true);
    }

    // Function to delete the camera with canvas
    private void DeleteCameraWithCanvas()
    {
        GameObject cameraWithCanvas = GameObject.FindGameObjectWithTag("MainCamera");
        if (cameraWithCanvas != null)
        {
            Destroy(cameraWithCanvas);
        }
    }
}
