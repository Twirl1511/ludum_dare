using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeRendering : MonoBehaviour
{
    private LineRenderer _line;
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private Transform _p1;
    [SerializeField] private Transform _p2;
    [SerializeField] private float _pointStep = 0.2f;
    private float _tension = .1f;
    private float _length;

    void Start()
    {
        _line = GetComponent<LineRenderer>();
        Vector3 basePos = _p1.position;
        Vector3 endPos = _p2.position;
        Vector3 direction = endPos - basePos;
        _length = direction.magnitude;
        int a = (int)(_length / _pointStep);
        _line.positionCount = a;
        direction.Normalize();
        for(int i = 0; i < _line.positionCount; i++)
        {
            //_line.SetPosition(i, basePos + direction * i * _pointStep + Vector3.down * i * _tension * _curve.Evaluate(_length / (i * ));
        }
        _line.SetPosition(0, basePos);
        _line.SetPosition(_line.positionCount - 1, endPos);

    }

    void Update()
    {
        
    }
}
