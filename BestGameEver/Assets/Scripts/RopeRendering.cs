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
    [SerializeField] private float _tension = 1f;

    private float _length;
    private Vector3 basePos;
    private Vector3 endPos;
    private Vector3 direction;

    private void Start()
    {
        _line = GetComponent<LineRenderer>();
        Init(_p1, _p2);
    }

    private void Init(Transform p1, Transform p2)
    {
        _p1 = p1;
        _p2 = p2;
        Reinit();
    }

    private void Reinit()
    {
        basePos = _p1.position;
        endPos = _p2.position;
        direction = endPos - basePos;
        _length = direction.magnitude;
        direction.Normalize();
        int pointCount = (int)(_length / _pointStep);
        _line.positionCount = pointCount;
    }

    private void Update()
    {
        Reinit();
        for (int i = 0; i < _line.positionCount; i++)
        {
            _line.SetPosition(i, (basePos + direction * i * _pointStep) + Vector3.down * _tension * (_curve.Evaluate((i * _pointStep) / _length)));
        }
        _line.SetPosition(0, basePos);
        _line.SetPosition(_line.positionCount - 1, endPos);
    }
}
