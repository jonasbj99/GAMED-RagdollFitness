using OpenCover.Framework.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehavior : MonoBehaviour
{
    [SerializeField] float waitSeconds = 3f;

    [SerializeField] Animator playWeightAnimator;
    [SerializeField] Animator settingsWeightAnimator;
    [SerializeField] Animator quitWeightAnimator;

    [SerializeField] Animator playButtonAnimator;
    [SerializeField] Animator settingsButtonAnimator;
    [SerializeField] Animator quitButtonAnimator;

    [SerializeField] Animator titleAnimator;
    [SerializeField] Animator devLogoAnimator;

    public GameObject pauseMenu; // Reference to the PauseMenu object in the hierarchy

    private void Update()
    {
        // Check for Escape key press
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(OutMainMenuAnimation());
        }

        if (Input.GetKeyDown (KeyCode.Z)) 
        { 
            StartCoroutine(InMainMenuAnimation());
        }
    }

    IEnumerator OutMainMenuAnimation()
    {
        OutAnimateButtons();

        yield return new WaitForSeconds(waitSeconds);

        OutStaticButtons();
    }

    IEnumerator InMainMenuAnimation()
    {
        InAnimateButtons();

        yield return new WaitForSeconds(waitSeconds);

        InStaticButtons();
    }

    void OutAnimateButtons()
    {
        playWeightAnimator.Play("Base Layer.fastClockwiseAnim", 0, 0);
        settingsWeightAnimator.Play("Base Layer.fastCounterAnim", 0, 0);
        quitWeightAnimator.Play("Base Layer.fastClockwiseAnim", 0, 0);

        playButtonAnimator.Play("Base Layer.moveRightAnim", 0, 0);
        settingsButtonAnimator.Play("Base Layer.moveLeftAnim", 0, 0);
        quitButtonAnimator.Play("Base Layer.moveRightAnim", 0, 0);

        titleAnimator.Play("Base Layer.titleMoveAnim", 0, 0);
        devLogoAnimator.Play("Base Layer.devMoveAnim", 0, 0);
    }

    void OutStaticButtons()
    {
        playButtonAnimator.Play("Base Layer.revStaticRightAnim", 0, 0);
        settingsButtonAnimator.Play("Base Layer.revStaticLeftAnim", 0, 0);
        quitButtonAnimator.Play("Base Layer.revStaticRightAnim", 0, 0);

        titleAnimator.Play("Base Layer.revStaticTitleAnim", 0, 0);
        devLogoAnimator.Play("Base Layer.revStaticDevAnim", 0, 0);
    }

    void InAnimateButtons()
    {
        playWeightAnimator.Play("Base Layer.fastCounterAnim", 0, 0);
        settingsWeightAnimator.Play("Base Layer.fastClockwiseAnim", 0, 0);
        quitWeightAnimator.Play("Base Layer.fastCounterAnim", 0, 0);

        playButtonAnimator.Play("Base Layer.reverseRightAnim", 0, 0);
        settingsButtonAnimator.Play("Base Layer.reverseLeftAnim", 0, 0);
        quitButtonAnimator.Play("Base Layer.reverseRightAnim", 0, 0);

        titleAnimator.Play("Base Layer.reverseTitleAnim", 0, 0);
        devLogoAnimator.Play("Base Layer.reverseDevAnim", 0, 0);
    }

    void InStaticButtons()
    {
        playWeightAnimator.Play("Base Layer.clockwiseAnim", 0, 0);
        settingsWeightAnimator.Play("Base Layer.counterAnim", 0, 0);
        quitWeightAnimator.Play("Base Layer.clockwiseAnim", 0, 0);

        playButtonAnimator.Play("Base Layer.staticRightAnim", 0, 0);
        settingsButtonAnimator.Play("Base Layer.staticLeftAnim", 0, 0);
        quitButtonAnimator.Play("Base Layer.staticRightAnim", 0, 0);

        titleAnimator.Play("Base Layer.staticTitleAnim", 0, 0);
        devLogoAnimator.Play("Base Layer.staticDevAnim", 0, 0);
    }

    void SwitchScene(string scene)
    {
        if (scene == "Quit")
        {
            Application.Quit();
        }
        //else
        {
            //SceneManager.LoadScene(scene);
        }
    }

    void PauseGame()
    {
        // Pause the game
        //Time.timeScale = 0f;

        // Activate the PauseMenu object
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true);
        }

        // Make the cursor visible and unlock it
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Method to resume the game
    public void ResumeGame()
    {
        // Set time scale back to normal
        //Time.timeScale = 1f;

        // Deactivate the PauseMenu object
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }

        // Hide and lock the cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Method to handle main menu button click
    public void MainMenuButton()
    {
        Debug.Log("Main Menu Button Clicked");

        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        if (mainCamera != null)
        {
            Debug.Log("Main Camera Found");
            mainCamera.SetActive(true);
        }
        else
        {
            Debug.Log("Main Camera NOT Found");
        }
    }

}


