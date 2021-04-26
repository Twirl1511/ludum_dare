using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUI : MonoBehaviour
{
    [SerializeField] private GameObject Canvas;
    void Start()
    {
        
    }

    private void OnMouseEnter()
    {
        Canvas.SetActive(true);
    }
    private void OnMouseExit()
    {
        Canvas.SetActive(false);
    }
}
