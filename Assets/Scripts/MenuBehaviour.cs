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

    public void PlayButton()
    {
        StartCoroutine(DelayScene(playScene));
    }

    IEnumerator DelayScene(String scene)
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

    void SwitchScene(String scene)
    {
        if (scene == "Quit")
        {
            Application.Quit();
        }
        else {
            SceneManager.LoadScene(scene);
        }
    }
}
