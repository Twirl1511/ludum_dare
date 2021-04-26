using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Population : MonoBehaviour
{
    public PlatformMove[] Platforms;
    public float TotalMass;
    public Text TotalPopulationText;
    public Text TotalDeathText;

    public float PopulationToWin;
    public float DeathToLoose;

    [SerializeField] private GameObject _creditsPanel;
    [SerializeField] private GameObject _exitButton;
    [SerializeField] private GameObject _backButton;

    [SerializeField] private GameObject _loosePanel;

    void Start()
    {
        InvokeRepeating(nameof(ShowPopulation), 1, 1);    
    }

    private void ShowPopulation()
    {
        TotalMass = 0;
        foreach (var platform in Platforms)
        {
            TotalMass += platform.Mass;
        }
        
        TotalPopulationText.text = TotalMass.ToString("0");
        TotalDeathText.text = Building.DeathCounter.ToString("0");

        if(TotalMass >= PopulationToWin)
        {
            _creditsPanel.SetActive(true);
            _exitButton.SetActive(true);
            _backButton.SetActive(false);
            Pause.State = Pause.States.Pause;
        }
        if (Building.DeathCounter >= DeathToLoose)
        {
            _loosePanel.SetActive(true);
            Pause.State = Pause.States.Pause;
        }
        

    }

}
