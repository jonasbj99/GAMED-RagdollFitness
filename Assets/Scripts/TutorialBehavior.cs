using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBehavior : MonoBehaviour
{
    [SerializeField] GameObject tutorialWindow;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            tutorialWindow.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void CloseButton ()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        tutorialWindow.SetActive(false);
    }
}
