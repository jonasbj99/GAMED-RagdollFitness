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

    public float raycastDistance = 2f;
    public LayerMask groundLayer;
    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        hips = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() 
    {
        if(Input.GetKey(KeyCode.W) && isGrounded)
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

        if(Input.GetKey(KeyCode.A) && isGrounded)
        {
            animator.SetBool("slideLeft", true);
            hips.AddForce(-hips.transform.right * strafeSpeed);
        }
        else
        {
            animator.SetBool("slideLeft", false);
        }

        if(Input.GetKey(KeyCode.S) && isGrounded)
        {
            animator.SetBool("isWalk", true);
            hips.AddForce(-hips.transform.forward * speed);
        }
        else if(!Input.GetKey(KeyCode.W))
        {
            animator.SetBool("isWalk", false);
        }

        if(Input.GetKey(KeyCode.D) && isGrounded)
        {      
            animator.SetBool("slideRight", true);
            hips.AddForce(hips.transform.right * strafeSpeed);
        }
        else
        {
            animator.SetBool("slideRight", false);
        }

        // Check if the player is grounded
        isGrounded = Physics.Raycast(transform.position, Vector3.down, raycastDistance, groundLayer);

        // Jump input
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            // Apply jump force if grounded and Space key is pressed
            hips.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
    }

}
