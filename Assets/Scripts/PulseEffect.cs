using UnityEngine;

public class PulseEffect : MonoBehaviour
{
    public float pulseSpeed = 1.0f; // Speed of the pulse effect
    public float maxAlpha = 0.5f;   // Maximum alpha value (transparency)

    private Material material;       // Reference to the object's material
    private Color originalColor;     // Original color of the object
    private float timer = 0.0f;      // Timer for the pulse effect

    void Start()
    {
        // Get the material attached to the object
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            material = renderer.material;
            originalColor = material.color;
        }
        else
        {
            Debug.LogError("PulseEffect script requires a Renderer component with a material.");
            enabled = false; // Disable the script if Renderer component is not found
        }
    }

    void Update()
    {
        // Calculate the new alpha value based on sine function for pulsing effect
        float alpha = maxAlpha * Mathf.Abs(Mathf.Sin(timer * pulseSpeed));
        Color newColor = originalColor;
        newColor.a = alpha;

        // Update the material's color
        material.color = newColor;

        // Increment the timer
        timer += Time.deltaTime;
    }
}
