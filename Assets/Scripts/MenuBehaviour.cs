using OpenCover.Framework.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehavior : MonoBehaviour
{
    [SerializeField] float waitSeconds = 2f;

    string playScene = "NewFitnessScene";
    string settingsScene = "Settings";
    string quitScene = "Quit";

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
    }

    public void PlayButton()
    {
        StartCoroutine(DelayScene(playScene));
    }

    public void QuitButton()
    {
        StartCoroutine(DelayScene(quitScene));
    }

    IEnumerator DelayScene(string scene)
    {
        AnimateButtons();

        yield return new WaitForSeconds(waitSeconds);

        //SwitchScene(scene);
    }

    void AnimateButtons()
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


