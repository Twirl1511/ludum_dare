using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAnimation : MonoBehaviour
{
    public float speedCoef = 0.1f;
    public Renderer _renderer;
    private Material _mat;
    public Vector2 _offset;
    public Rope _rope;
    [SerializeField] private ElementSpeed[] _offsetSpeed;

    public float speed;

    void Start()
    {
        _offset = Vector2.zero;
        _mat = _renderer.material;
        InvokeRepeating(nameof(UpdateSpeed), 0f, 0.5f);
    }
    

    void Update()
    {
        _offset.x += Time.deltaTime * speed  * speedCoef;
        _mat.SetTextureOffset("_MainTex", _offset);
    }

    private void UpdateSpeed()
    {
        for (int i = 0; i < _offsetSpeed.Length; i++)
        {
            if (_rope._currentSuckIndex == _offsetSpeed[i].Element)
            {
                speed = _offsetSpeed[i].Offset;
                break;
            }
        }
    }

    [System.Serializable]
    public struct ElementSpeed
    {
        public float Element;
        public float Offset;
    }
}
