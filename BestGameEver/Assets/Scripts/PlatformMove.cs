using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlatformMove : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            MoveDown(1f);
        }
    }

    public void MoveDown(float mass)
    {
        float y = transform.position.y;
        
            transform.DOMoveY(y - mass, speed);
    }
}
