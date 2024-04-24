using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;

    public float speed;
    public float strafeSpeed;
    public float jumpForce;

    public Rigidbody hips;
    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        hips = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() 
    {
        if(Input.GetKey(KeyCode.W))
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetBool("isWalk", true);
                animator.SetBool("isRun", true);
                hips.AddForce(hips.transform.forward * speed * 1.5f);
            }
            else
            {
                animator.SetBool("isRun", false);
                animator.SetBool("isWalk", true);
                hips.AddForce(hips.transform.forward * speed);
            }
        } else 
        {
            animator.SetBool("isWalk", false);
            animator.SetBool("isRun", false);  
        }

        if(Input.GetKey(KeyCode.A))
        {
            animator.SetBool("slideLeft", true);
            hips.AddForce(-hips.transform.right * strafeSpeed);
        }
        else
        {
            animator.SetBool("slideLeft", false);
        }

        if(Input.GetKey(KeyCode.S))
        {
            animator.SetBool("isWalk", true);
            hips.AddForce(-hips.transform.forward * speed);
        }
        else if(!Input.GetKey(KeyCode.W))
        {
            animator.SetBool("isWalk", false);
        }

        if(Input.GetKey(KeyCode.D))
        {      
            animator.SetBool("slideRight", true);
            hips.AddForce(hips.transform.right * strafeSpeed);
        }
        else
        {
            animator.SetBool("slideRight", false);
        }

        if(Input.GetAxis("Jump") > 0)
        {
            if(isGrounded)
            {
                hips.AddForce(new Vector3(0, jumpForce, 0));
                isGrounded = false;
            }
        }
    }

}
