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
        }
    }

    void rankChange()
    {
        for (int i = 0; i < rankArray.Length; i++)
        {
            rankArray[i].SetActive(false);
        }

        rankArray[currentRank-1].SetActive(true);
    }

    void SetSlider()
    {
        strengthSlider.maxValue = maxStrength;
        strengthSlider.value = currentStrength;
    }
}
