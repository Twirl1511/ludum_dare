using System.Collections;
using UnityEngine;
using DG.Tweening;

public class PlatformMove : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    //[SerializeField] private float updateSpeed = 10f;
    [SerializeField] private float moveDistance = 10f;

    [SerializeField] private Dependency[] _fallCD;
    [SerializeField] private float _cd = 1f;
    [SerializeField] private float _deltaTimer = -1f;

    // Mass = 0   |   Height = 0
    // Mass = 2   |   Height = -0.2
    // Mass = 10   |   Height = -1.0

    private float _prevMass = 0f;
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
            SetNewCD();
        }
    }
    private Vector3 _startPosition;

    public void InitFall()
    {
        _startPosition = transform.position;
        _deltaTimer = 0f;
        //StartCoroutine(MoveDown());
    }

    void Update()
    {
        if (_deltaTimer >= 0f)
        {
            _deltaTimer += Time.deltaTime;

            if(_deltaTimer >= _cd)
            {
                float sign = Mathf.Sign(_mass - _prevMass);
                float deltaMassAbs = Mathf.Abs(_mass - _prevMass);
                if (deltaMassAbs >= 10)
                {
                    _prevMass = _mass;
                    float newY = transform.position.y - moveDistance * sign;
                    transform.DOMoveY(newY, speed).SetEase(Ease.Linear);
                    SetNewCD();
                }
                _deltaTimer = 0f;
            }
        }
    }

    public IEnumerator MoveDown()
    {
        while (true)
        {
            //_deltaTimer = 0f;
            yield return new WaitForSeconds(_cd);
            //float deltaMass = Mathf.Abs(_mass - _prevMass);
            //if (deltaMass >= 10)
            //{
            //    _prevMass = _mass;
            //    float newY = _startPosition.y - moveDistance;
            //    transform.DOMoveY(newY, speed).SetEase(Ease.Linear);
            //    SetNewCD();
            //}
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
