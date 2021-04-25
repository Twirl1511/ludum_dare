using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population : MonoBehaviour
{
    public PlatformMove[] Platforms;
    public float TotalMass;
    public Text TotalPopulationText;
    public Text TotalDeathText;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TotalMass = 0;
        foreach (var platform in Platforms)
        {
            TotalMass += platform.Mass;
        }
        TotalPopulationText.text = TotalMass.ToString("0");
        //TotalDeathText.text = 
    }

}
