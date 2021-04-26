using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeConnect : MonoBehaviour
{
    public LayerMask LayerMask;
    public PipeConnectionController controller;
    public Rope rope;

    private void OnMouseDown()
    {
        controller._selected = this;

    }
}
