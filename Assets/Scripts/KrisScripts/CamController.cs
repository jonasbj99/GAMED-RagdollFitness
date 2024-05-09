using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    // bool isGrabbing = false;

    public float rotationSpeed = 1;
    public Transform root;

    float mouseX, mouseY;

    public float stomachOffset;

    public ConfigurableJoint hipJoint, stomachJoint, leftShoulder, rightShoulder;

    public float minRotationX = 30f;
    public float maxRotationX = 45f;
    public float minRotationY = 0f;
    public float maxRotationY = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    void FixedUpdate()
    {
        CamControl();
    }


    void CamControl()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed; // Horizontal movement controls y-axis rotation
        mouseY += Input.GetAxis("Mouse Y") * rotationSpeed; // Vertical movement controls x-axis rotation
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        Quaternion rootRotation = Quaternion.Euler(0, mouseX, mouseY);

        root.rotation = rootRotation;
        
        root.rotation = rootRotation;
        hipJoint.targetRotation = Quaternion.Euler(0, -mouseX, 0);    
            
        stomachJoint.targetRotation = Quaternion.Euler(0, 0, -mouseY + stomachOffset);

        
        float clampedMouseX = Mathf.Clamp(mouseX, minRotationX, maxRotationX);
        float clampedMouseY = Mathf.Clamp(mouseY, minRotationY, maxRotationY);

        // leftShoulder.targetRotation = Quaternion.Euler(0, -clampedMouseX, -mouseY);
        // rightShoulder.targetRotation = Quaternion.Euler(0, -clampedMouseX -180, -mouseY); 

        leftShoulder.targetRotation = Quaternion.Euler(0, -90, -mouseY);
        rightShoulder.targetRotation = Quaternion.Euler(0, 90, -mouseY); 
        
        
        /*if (Input.GetKey(KeyCode.Z))
        {
            mouseX += Input.GetAxis("Mouse X") * rotationSpeed; // Horizontal movement controls y-axis rotation
            mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed; // Vertical movement controls x-axis rotation
            mouseY = Mathf.Clamp(mouseY, -35, 60);

            Quaternion rootRotation = Quaternion.Euler(0, mouseX, mouseY);

            root.rotation = rootRotation;
        
            root.rotation = rootRotation;
            hipJoint.targetRotation = Quaternion.Euler(0, -mouseX, 0);

            if(Grab.alreadyGrabbing == false)
            {
            stomachJoint.targetRotation = Quaternion.Euler(0, 0, -mouseY + stomachOffset);
            }    
        }*/

    }
}

