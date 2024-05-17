using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StrengthBar : MonoBehaviour
{
    [SerializeField] Slider strengthSlider;

    [SerializeField] GameObject weightFollow;
    [SerializeField] GameObject weightSprite;

    [SerializeField] GameObject[] rankArray;
    [SerializeField] GameObject[] scaleObjects; // Array to hold game objects whose scales will increase

    [SerializeField] AudioSource rankAudio;

    float gameTimer = 0;
    bool gameFinished = false;
    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text finishTimerText;
    [SerializeField] GameObject finishScreen;

    public static int currentStrength;
    int startStrength = 0;
    int maxStrength = 10;
    int currentRank = 1;

    private void Start()
    {
        currentStrength = startStrength;
        strengthSlider.maxValue = maxStrength;
        currentRank = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            currentStrength += 3;
        }

        /*
        timerText.text = gameTimer.ToString("00:00.00");

        if (currentStrength >= maxStrength)
        {
            rankUp();
        }
        */

        //SetSlider();

        //weightSprite.transform.position = weightFollow.transform.position;

        if (!gameFinished)
        {
            if (currentStrength >= maxStrength)
            {
                rankUp();
            }

            SetSlider();

            gameTimer += Time.deltaTime;
        }
        else if (gameFinished)
        {
            finishTimerText.text = timerText.text;
            finishScreen.SetActive(true);
        }

        weightSprite.transform.position = weightFollow.transform.position;

        timerText.text = gameTimer.ToString("00:00.00");
    }

    void rankUp()
    {
        if (currentRank < 9)
        {
            currentRank += 1;
            currentStrength -= maxStrength;
            maxStrength += 5;

            rankChange();
            adjustScale(); // Call method to adjust scale of game objects
            rankAudio.Play();
        } else if (currentRank == 9) {
            currentRank = 10;
            rankChange();
            gameFinished = true;
        }
    }

    void rankChange()
    {
        for (int i = 0; i < rankArray.Length; i++)
        {
            rankArray[i].SetActive(false);
        }

        rankArray[currentRank - 1].SetActive(true);
    }

    void SetSlider()
    {
        strengthSlider.maxValue = maxStrength;
        strengthSlider.value = currentStrength;
    }

    void adjustScale()
    {
        // Increase the scale of each object slightly
        foreach (GameObject obj in scaleObjects)
        {
            Vector3 currentScale = obj.transform.localScale;
            currentScale *= 1.03f; // Increase scale by 3%
            obj.transform.localScale = currentScale;

            Rigidbody rigidbody = obj.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                // Assuming each object has a configurable joint
                ConfigurableJoint joint = obj.GetComponent<ConfigurableJoint>();
                if (joint != null && joint.connectedBody != null)
                {
                    // Adjust mass scale of the joint
                    joint.massScale *= 1.03f; // Increase mass scale by 3%
                    joint.connectedMassScale = 1f; // Optionally adjust connected mass scale if needed
                }
            }
        }
    }

}
