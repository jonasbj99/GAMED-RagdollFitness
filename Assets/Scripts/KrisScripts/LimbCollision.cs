using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbCollision : MonoBehaviour
{
   public PlayerController playerController;


   void Start()
   {
        playerController = GameObject.FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
   }

   private void OnCollisionEnter(Collision other) 
   {
        playerController.isGrounded = true;
   }
}
