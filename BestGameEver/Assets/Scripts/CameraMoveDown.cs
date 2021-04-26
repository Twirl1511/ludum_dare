using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMoveDown : MonoBehaviour
{
    public float time;
    public float deadLine;
    void Start()
    {
        InvokeRepeating(nameof(MoveDown), time, time);
    }

    private void MoveDown()
    {
        if (transform.position.y >= deadLine - 1)
        {
            transform.DOMoveY(transform.position.y - 1, 5);
        }
        
        
    }
}
