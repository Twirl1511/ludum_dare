using UnityEngine;
using DG.Tweening;

public class PlatformMove : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private float updateSpeed = 10f;
    [SerializeField] private float fallDistanceModifier = 10f;

    // Mass = 0   |   Height = 0
    // Mass = 2   |   Height = -0.2
    // Mass = 10   |   Height = -1.0

    private float _prevMass = 0f;
    public float _mass = 0f;
    private Vector3 _startPosition;

    public void InitFall()
    {
        _startPosition = transform.position;
        InvokeRepeating(nameof(MoveDown), updateSpeed, updateSpeed);
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

    public void MoveDown()
    {
        float deltaMass = Mathf.Abs(_mass - _prevMass);
        if (deltaMass < 10)
            return;
        _prevMass = _mass;
        float newY = _startPosition.y - _mass / fallDistanceModifier;
        transform.DOMoveY(newY, speed).SetEase(Ease.Linear);
    }
}
