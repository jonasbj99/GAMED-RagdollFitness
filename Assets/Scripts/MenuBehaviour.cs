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

    [SerializeField] Animator playMove;
    [SerializeField] Animator settingsMove;
    [SerializeField] Animator quitMove;
    [SerializeField] Animator playWeight;
    [SerializeField] Animator settingsWeight;
    [SerializeField] Animator quitWeight;

    public void PlayButton()
    {
        StartCoroutine(DelayScene(playScene));
    }

    IEnumerator DelayScene(String scene)
    {
        AnimateButtons();

        yield return new WaitForSeconds(waitSeconds);

        SwitchScene(scene);
    }

    void AnimateButtons()
    {
        playWeight.Play("Base Layer.fastClockwiseAnim", 0, 0);
        settingsWeight.Play("Base Layer.fastCounterAnim", 0, 0);
        quitWeight.Play("Base Layer.fastClockwiseAnim", 0, 0);
        playMove.Play("Base Layer.moveRightAnim", 0, 0);
        settingsMove.Play("Base Layer.moveLeftAnim", 0, 0);
        quitMove.Play("Base Layer.moveRightAnim", 0, 0);
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
