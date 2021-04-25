using System.Collections;
using UnityEngine;
using DG.Tweening;

public class PlatformMove : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private float moveDistance = 10f;

    [SerializeField] private Dependency[] _fallCD;
    [SerializeField] private float _cd = 1f;
    [SerializeField] private float _deltaTimer = -1f;

    // Mass = 0   |   Height = 0
    // Mass = 2   |   Height = -0.2
    // Mass = 10   |   Height = -1.0

    private Vector3 _startPosition;

    private float _maxMass = 0f;
    private float _mass = 0f;
    public float Mass
    {
        get
        {
            return _mass;
        }
        set
        {
            _mass = value;
            if (_maxMass < _mass)
                _maxMass = _mass;
            SetNewCD();
        }
    }

    private void Start()
    {
        _startPosition = transform.position;
    }

    public void InitFall()
    {
        _startPosition = transform.position;
        _deltaTimer = 0f;
    }

    void Update()
    {
        if (_deltaTimer >= 0f)
        {
            _deltaTimer += Time.deltaTime;

            if(_deltaTimer >= _cd)
            {
                float newY = _startPosition.y - (_maxMass / 50) * moveDistance;
                transform.DOMoveY(newY, speed).SetEase(Ease.Linear);
                SetNewCD();
                _deltaTimer = 0f;
            }
        }
    }

    private void SetNewCD()
    {
        for(int i = 0; i < _fallCD.Length; i++)
        {
            if (_mass > _fallCD[i].Humans)
                continue;
            if (_mass < _fallCD[i].Humans)
            {
                _cd = _fallCD[i].CD;
                break;
            }
        }
    }

    [System.Serializable]
    public struct Dependency
    {
        public float Humans;
        public float CD;
    }
}
