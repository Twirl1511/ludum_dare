﻿using UnityEngine;
using DG.Tweening;

public class PlatformMove : MonoBehaviour
{
    [SerializeField] private float speed = 1;

    // Mass = 0   |   Height = 0
    // Mass = 2   |   Height = -0.2
    // Mass = 10   |   Height = -1.0

    [SerializeField] private float _mass = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            MoveDown();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            _mass++;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _mass--;
        }
    }

    public void MoveDown()
    {
        float newY = 0f - _mass / 10f;
        transform.DOMoveY(newY, speed);
    }
}
