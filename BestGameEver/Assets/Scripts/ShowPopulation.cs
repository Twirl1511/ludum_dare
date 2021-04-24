using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPopulation : MonoBehaviour
{
    [SerializeField] private Text _populationCount;
    [HideInInspector] public PlatformMove _platform;
    void Start()
    {
        
    }


    void Update()
    {
        _populationCount.text = _platform._mass.ToString();
    }
}
