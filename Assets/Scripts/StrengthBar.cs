using System.Collections;
using System.Collections.Generic;
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
        if (currentStrength >= maxStrength)
        {
            rankUp();
        }

        //if (Input.GetKeyDown(KeyCode.Space)) 
        {
            // currentStrength += 7;
        }

        SetSlider();

        weightSprite.transform.position = weightFollow.transform.position;
    }

    void rankUp()
    {
        if (currentRank < 10)
        {
            currentRank += 1;
            currentStrength -= maxStrength;
            maxStrength += 5;

            rankChange();
            adjustScale(); // Call method to adjust scale of game objects
            rankAudio.Play();
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
            currentScale *= 1.03f; // Increase scale by 10%
            obj.transform.localScale = currentScale;
        }
    }
}
