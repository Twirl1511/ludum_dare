using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparent : MonoBehaviour
{
    [SerializeField] private Material standartMaterial;
    [SerializeField] private Material transparentMaterial;

    private Color _platformColor;
    private Color _platformColorTransparent;
    private Color _platformColorDefault;
    private Renderer renderer;
    void Start()
    {
        renderer = GetComponent<Renderer>();
        _platformColor = renderer.material.color;
        float r = _platformColor.r;
        float g = _platformColor.g;
        float b = _platformColor.b;
        _platformColorTransparent = new Color(r, g, b, 0f);
        _platformColorDefault = new Color(r, g, b, 1f);
    }

    private void OnMouseEnter()
    {
        if(enabled)
            renderer.material = transparentMaterial;
        //renderer.material.SetColor("_Color", _platformColorTransparent);
    }
    private void OnMouseExit()
    {
        if(enabled)
            renderer.material = standartMaterial;
        //renderer.material.SetColor("_Color", _platformColorDefault);
    }
}


