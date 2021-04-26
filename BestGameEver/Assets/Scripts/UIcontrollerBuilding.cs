using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIcontrollerBuilding : MonoBehaviour
{
    public Text TotalPopulation;
    public Text Income;
    public Text Leave;
    public Text CurrentToDown;
    public Text LimitToDown;
    private PlatformMove platform;
    public float LimitToDownFloat = 50f;
    private float CurrentMassDownFloat = 0;
    void Start()
    {
        platform = GetComponent<Building>()._platform;
        
    }

    void Update()
    {
        Show();
    }
    public void Show()
    {
        CurrentMassDownFloat = platform.Mass;

        CurrentToDown.text = CurrentMassDownFloat.ToString("0");
    }
}
