using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPopulation : MonoBehaviour
{
    [SerializeField] private Text _populationCount;
    [SerializeField] private Text _incomeCount;
    public Building building;
    public PlatformMove _platform;


    void Update()
    {
        _platform = building._platform;
        _populationCount.text = _platform.Mass.ToString("0");
        _incomeCount.text = building._production.ToString("0");
    }
}
