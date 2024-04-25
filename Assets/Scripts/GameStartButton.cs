using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public GameObject cameraWithCanvasPrefab; // Assign your camera with canvas prefab in the Inspector
    public GameObject player; // Assign your player prefab in the Inspector
    public GameObject playerExtra; // Assign your player extra prefab in the Inspector

    // Function called when the first button is clicked
    public void OnButtonClick()
    {
        // Get reference to the camera with canvas
        GameObject cameraWithCanvas = GameObject.FindGameObjectWithTag("MainCamera");

        // Delay the camera disabling by 4 seconds
        Invoke("DisableCameraWithCanvas", 2f);

        // Spawn a new player prefab
        player.SetActive(true);
        // Spawn a new player extra prefab
        playerExtra.SetActive(true);
    }

    // Function called when the second button is clicked
    public void OnReverseButtonClick()
    {
        GameObject cameraWithCanvas = GameObject.FindGameObjectWithTag("MainCamera");

        Invoke("EnableCameraWithCanvas", 2f);
        // Deactivate the player prefab
        player.SetActive(false);
        // Deactivate the player extra prefab
        playerExtra.SetActive(false);
    }


    // Function to disable the camera withav canvas
    private void DisableCameraWithCanvas()
    {
        GameObject cameraWithCanvas = GameObject.FindGameObjectWithTag("MainCamera");
        if (cameraWithCanvas != null)
        {
            cameraWithCanvas.SetActive(false);
        }
    }

    private void EnableCameraWithCanvas()
    {
        GameObject cameraWithCanvas = GameObject.FindGameObjectWithTag("MainCamera");
        if (cameraWithCanvas != null)
        {
            cameraWithCanvas.SetActive(true);
        }
    }
}
