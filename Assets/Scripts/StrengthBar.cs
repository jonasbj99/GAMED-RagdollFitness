using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrengthBar : MonoBehaviour
{
    [SerializeField] Slider strengthSlider;

    [SerializeField] GameObject weightFollow;
    [SerializeField] GameObject weightSprite;

    int currentStrength;
    int startStrength = 0;
    int maxStrength = 10;

    private void Start()
    {
        currentStrength = startStrength;
        strengthSlider.maxValue = maxStrength;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentStrength++;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            currentStrength = 0;
        }

        if (Input.GetKeyDown (KeyCode.X)) 
        {
            maxStrength += 5;
        }

        SetStrength(currentStrength);

        weightSprite.transform.position = weightFollow.transform.position;
    }

    void SetStrength(int strValue)
    {
        strengthSlider.maxValue = maxStrength;
        strengthSlider.value = strValue;
    }
}
