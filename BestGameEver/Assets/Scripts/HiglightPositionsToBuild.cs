using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HiglightPositionsToBuild : MonoBehaviour
{
    private Color _positionColor;
    private Color _positionColorHighlight;
    private Color _positionColorDefault;
    [SerializeField] private Renderer renderer;
    private Vector3 _position;
    void Start()
    {
        //GetComponent<SphereCollider>().radius = BuildingController.singleton._ropeLength;
        _positionColor = renderer.material.color;
        float r = _positionColor.r;
        float g = _positionColor.g;
        float b = _positionColor.b;
        _positionColorHighlight = new Color(r, g, b, 0.4f);
        _positionColorDefault = new Color(r, g, b, 0f);
        _position = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PositionToBuild"))
        {
            other.GetComponent<Renderer>().material.SetColor("_Color", _positionColorHighlight);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PositionToBuild"))
        {
            other.GetComponent<Renderer>().material.SetColor("_Color", _positionColorDefault);
        }
    }



    public void HiglightOff()
    {
        //transform.DOMoveY(30, 1).From();
        transform.position += Vector3.up * 50f;
        StartCoroutine(SetAtciveFalse());
    }


    IEnumerator SetAtciveFalse()
    {
        yield return new WaitForSeconds(0.05f);
        gameObject.SetActive(false);
        transform.position -= Vector3.up * 50f;
    }
}
