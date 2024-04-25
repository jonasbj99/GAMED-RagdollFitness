using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrengthBar : MonoBehaviour
{
    [SerializeField] Slider strengthSlider;

    [SerializeField] GameObject weightFollow;
    [SerializeField] GameObject weightSprite;

    int currentStrength = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentStrength++;
        }

        SetStrength(currentStrength);

        weightSprite.transform.position = weightFollow.transform.position;
    }

    void SetStrength(int strValue)
    {
        strengthSlider.value = strValue;
    }
}
