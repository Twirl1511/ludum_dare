using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Population : MonoBehaviour
{
    public PlatformMove[] Platforms;
    public float TotalMass;
    public Text TotalPopulationText;

    void Start()
    {
        InvokeRepeating(nameof(CalculatePopulation), 1, 1);
    }


    private void CalculatePopulation()
    {
        TotalMass = 0;
        foreach (var platform in Platforms)
        {
            TotalMass += platform.Mass;
        }
        TotalPopulationText.text = TotalMass.ToString("0");
    }

}
