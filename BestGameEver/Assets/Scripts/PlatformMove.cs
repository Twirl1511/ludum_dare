using System.Collections;
using UnityEngine;
using DG.Tweening;

public class PlatformMove : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    //[SerializeField] private float updateSpeed = 10f;
    [SerializeField] private float moveDistance = 10f;

    [SerializeField] private Dependency[] _fallCD;
    private float _cd = 1f;

    // Mass = 0   |   Height = 0
    // Mass = 2   |   Height = -0.2
    // Mass = 10   |   Height = -1.0

    private float _prevMass = 0f;
    public float _mass = 0f;
    private Vector3 _startPosition;

    public void InitFall()
    {
        _startPosition = transform.position;
        StartCoroutine(MoveDown());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            MoveDown();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            _mass++;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _mass--;
        }
    }

    public IEnumerator MoveDown()
    {
        while (true)
        {
            yield return new WaitForSeconds(_cd);

            float deltaMass = Mathf.Abs(_mass - _prevMass);
            if (deltaMass >= 10)
            {
                _prevMass = _mass;
                float newY = _startPosition.y - moveDistance;
                transform.DOMoveY(newY, speed).SetEase(Ease.Linear);
                SetNewCD();
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
