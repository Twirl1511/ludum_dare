using UnityEngine;

public class RopeRendering : MonoBehaviour
{
    [SerializeField] private ParticleSystem _humanParticles;
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] public Transform _p1;
    [SerializeField] public Transform _p2;
    [SerializeField] private float _pointStep = 0.2f;
    [SerializeField] private float _tension = 1f;
    [SerializeField] private PipeConnect _pipeConnect;

    [HideInInspector] public float _length;
    //[HideInInspector] public Building _building;
    private Vector3 basePos;
    private Vector3 endPos;
    private Vector3 direction;
    private LineRenderer _line;
    private bool _broken;

    public Building buildingFrom;
    public Building buildingTo;

    public Vector3 GetMiddlePos() => _line.GetPosition(_line.positionCount / 2 + 1);

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
        _line = GetComponent<LineRenderer>();
        _line.positionCount = 0;
    }

    public void SetActive(bool active)
    {
        _line.enabled = active;
    }

    public void BrokePipe(Building from)
    {
        buildingFrom = from;
        buildingTo = null;

        _broken = true;
        _p1.parent = null;
        _p1.position = GetMiddlePos();
        Quaternion rot = Quaternion.LookRotation((_p2.position - _p1.position).normalized);
        _p1.rotation = rot;
        _humanParticles.transform.position = _p1.position;
        _humanParticles.transform.parent = _p1;
        _humanParticles.Play();

        _pipeConnect.gameObject.SetActive(true);
        _pipeConnect.transform.position = _p1.position;
        _pipeConnect.transform.parent = _p1;
    }

    public void Init(Building from, Building to)
    {
        buildingFrom = from;
        buildingTo = to;

        SetRopeBase();
    }

    private void Reinit()
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

    private void SetRopeBase()
    {
        Transform t = buildingFrom.PipeInputs[0];
        foreach (Transform p in buildingFrom.PipeInputs)
        {
            if ((buildingTo.transform.position - p.position).magnitude < (buildingTo.transform.position - t.position).magnitude)
            {
                t = p;
            }
        }
        _p2.position = t.position;
        _p2.parent = t;

        t = buildingTo.PipeInputs[0];
        foreach (Transform p in buildingTo.PipeInputs)
        {
            if ((_p1.parent.position - p.position).magnitude < (_p1.parent.position - t.position).magnitude)
            {
                t = p;
            }
        }

        _p1.position = t.position;
        _p1.parent = t.transform;
    }

    private void Update()
    {
        Reinit();
        DrawPipe();
    }

    private void DrawPipe()
    {
        for (int i = 0; i < _line.positionCount; i++)
        {
            _line.SetPosition(i, (basePos + direction * i * _pointStep) + Vector3.down * _tension * (_curve.Evaluate((i * _pointStep) / _length)));
        }
        _line.SetPosition(0, basePos);
        _line.SetPosition(_line.positionCount - 1, endPos);
    }
}
