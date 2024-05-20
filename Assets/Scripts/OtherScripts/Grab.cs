using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    public Animator animator;
    GameObject grabbedObj;
    public Rigidbody rb;
    public int isLeftorRight;
    
    public bool alreadyGrabbing = false;
    public bool rightGrab = false;
    public bool canGrab = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canGrab)
        {
            if (Input.GetMouseButton(1))
            {
                rightGrab = true;
            }
            else
            {
                rightGrab = false;
                Destroy(GetComponent<FixedJoint>());
            }
            
        }



        /*if(Input.GetMouseButtonDown(isLeftorRight))
        {
            
            if(isLeftorRight == 0)
            {
                //animator.SetBool("leftHandUp", true);
                grab = true;
                

            } else if(isLeftorRight == 1)
            {
                //animator.SetBool("rightHandUp", true);
                grab = true;
            }

            if(grabbedObj != null && !grabbedObj.GetComponent<FixedJoint>())
        {
            FixedJoint fj = grabbedObj.AddComponent<FixedJoint>();
            fj.connectedBody = rb;
            fj.breakForce = 9000;
        } 

        } else if(Input.GetMouseButtonUp(isLeftorRight))
        {
            
            if(isLeftorRight == 0)
            {
                //animator.SetBool("leftHandUp", false);
                grab = false;
            } else if(isLeftorRight == 1)
            {
                //animator.SetBool("rightHandUp", false);
                grab = false;
            }    
            if(grabbedObj != null)
            {
                Destroy(grabbedObj.GetComponent<FixedJoint>());
            }

            grabbedObj = null;
        }*/
    }

    private void OnCollisionEnter(Collision col)
    {
        if (rightGrab && col.transform.tag != "Player")
        {
            Rigidbody rb = col.transform.GetComponent<Rigidbody>();
            if (rb != null)
            {
                FixedJoint fj = transform.gameObject.AddComponent(typeof(FixedJoint)) as FixedJoint;
                fj.connectedBody = rb;
                fj.breakForce = 9000;
            }
            else
            {
                FixedJoint fj = transform.gameObject.AddComponent(typeof(FixedJoint)) as FixedJoint;
            }
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Item"))
        {
            grabbedObj = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        grabbedObj = null;
    }*/

}
