using UnityEngine;

public class RopeRendering : MonoBehaviour
{
    private LineRenderer _line;
    [SerializeField] private AnimationCurve _curve;
    private Transform _p1;
    private Transform _p2;
    [SerializeField] private float _pointStep = 0.2f;
    [SerializeField] private float _tension = 1f;

    [HideInInspector] public float _length;
    private Vector3 basePos;
    private Vector3 endPos;
    private Vector3 direction;

    public void SetPos1(Transform target) => _p1 = target;
    public void SetPos2(Transform target) => _p2 = target;

    private void Start()
    {
        _line = GetComponent<LineRenderer>();
        _line.positionCount = 0;
    }

    public void SetActive(bool active)
    {
        _line.enabled = active;
    }

    public void Init()
    {
        if(_line == null)
            _line = GetComponent<LineRenderer>();
        basePos = _p1.position;
        endPos = _p2.position;
        direction = endPos - basePos;
        _length = direction.magnitude;
        direction.Normalize();
        int pointCount = (int)(_length / _pointStep);
        _line.positionCount = pointCount;
    }

    public Vector3 GetMiddlePos()
    {
        return _line.GetPosition(_line.positionCount / 2 + 1);
    }

    private void Update()
    {
        if (_p1 != null)
        {
            Init();
            for (int i = 0; i < _line.positionCount; i++)
            {
                _line.SetPosition(i, (basePos + direction * i * _pointStep) + Vector3.down * _tension * (_curve.Evaluate((i * _pointStep) / _length)));
            }
            _line.SetPosition(0, basePos);
            _line.SetPosition(_line.positionCount - 1, endPos);
        }
    }
}
