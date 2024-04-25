using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    bool isGrabbing = false;

    public float rotationSpeed = 1;
    public Transform root;

    float mouseX, mouseY;

    public float stomachOffset;

    public ConfigurableJoint hipJoint, stomachJoint;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    void FixedUpdate()
    {
        CamControl();

        if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            isGrabbing = true;
        }
        
        if(Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            isGrabbing = false;
        }

        if(isGrabbing == true)
        {
        
        }
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

