using UnityEngine;

public class RopeRendering : MonoBehaviour
{
    private LineRenderer _line;
    [SerializeField] private ParticleSystem _humanParticles;
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] public Transform _p1;
    [SerializeField] public Transform _p2;
    [SerializeField] private float _pointStep = 0.2f;
    [SerializeField] private float _tension = 1f;

    [HideInInspector] public float _length;
    [HideInInspector] public Building _building;
    private Vector3 basePos;
    private Vector3 endPos;
    private Vector3 direction;
    private bool _broken;

    public void SetRopeBase(Building target)
    {
        Transform t = _building.PipeInputs[0];
        foreach(Transform p in _building.PipeInputs)
        {
            if((target.transform.position - p.position).magnitude < (target.transform.position - t.position).magnitude)
            {
                t = p;
            }
        }
        _p2.position = t.position;
        _p2.parent = t;

        t = target.PipeInputs[0];
        foreach (Transform p in target.PipeInputs)
        {
            if ((_p1.parent.position - p.position).magnitude < (_p1.parent.position - t.position).magnitude)
            {
                t = p;
            }
        }

        _p1.position = t.position;
        _p1.parent = t.transform;
    }

    public void SetPos1(Transform target)
    {
        _p1.position = target.position;
        _p1.parent = target;
    }
    public void SetPos2(Transform target)
    {
        _p2.position = target.position;
        _p2.parent = target;
    }

    private void Start()
    {
        _building = GetComponent<Building>();
        _line = GetComponent<LineRenderer>();
        _line.positionCount = 0;
    }

    public void SetActive(bool active)
    {
        _line.enabled = active;
    }

    public void BrokePipe()
    {
        _broken = true;
        _p2.parent = null;
        _p2.position = GetMiddlePos();
        Quaternion rot = Quaternion.LookRotation((_p2.position - _p1.position).normalized);
        _p2.rotation = rot;
        _humanParticles.Play();
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
        if (_building.HasPipe)
        {
            if (_broken)
            {
                Init();
                for (int i = 0; i < _line.positionCount; i++)
                {
                    _line.SetPosition(i, (basePos + direction * i * _pointStep) + Vector3.down * _tension * (_curve.Evaluate((i * _pointStep) / _length)));
                }
                _line.SetPosition(0, basePos);
                _line.SetPosition(_line.positionCount - 1, endPos);
            }
            else
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
}
