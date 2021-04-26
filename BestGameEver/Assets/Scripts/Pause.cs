using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    
    public enum States
    {
        Pause,
        Play
    }
    public static States State;

    void Start()
    {
    }

    
    void Update()
    {
        

        if(State == States.Pause)
        {
            Time.timeScale = 0;
        }
        if (State == States.Play)
        {
            Time.timeScale = 1;
        }
    }
}
