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
    [SerializeField] private float _maxMassIncriment = 10f;
    public static bool _isFirstUpdate = true;
    

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
            UpdatePos();
            //SetNewCD();
        }
    }

    private void Start()
    {
        _startPosition = transform.position;
    }

    //public void InitFall()
    //{
    //    _startPosition = transform.position;
    //    _deltaTimer = 0f;
    //}
    private float buffer;
    private bool flag = false;
    void UpdatePos()
    {
        //if (_deltaTimer >= 0f)
        //{
        //    _deltaTimer += Time.deltaTime;

        //    if(_deltaTimer >= _cd)
        //    {
        float newY = _startPosition.y - ((int)_maxMass / (int)50) * moveDistance;
        if (buffer != newY && flag)
        {
            AudioController.singleton.PlayPlatformSound();
            Invoke(nameof(GetDownHint), 1);
        }
        flag = true;
        transform.DOMoveY(newY, speed).SetEase(Ease.Linear);
        buffer = newY;


        
            
        

        //SetNewCD();
        //_deltaTimer = 0f;
        //    }
        //}
    }
    public void GetDownHint()
    {
        if (_isFirstUpdate)
        {
            MenuController.singleton.ShowHint();
            _isFirstUpdate = false;
        }
    }

    //private void SetNewCD()
    //{
    //    for(int i = 0; i < _fallCD.Length; i++)
    //    {
    //        if (_mass > _fallCD[i].Humans)
    //            continue;
    //        if (_mass < _fallCD[i].Humans)
    //        {
    //            _cd = _fallCD[i].CD;
    //            break;
    //        }
    //    }
    //}

    [System.Serializable]
    public struct Dependency
    {
        public float Humans;
        public float CD;
    }
}
