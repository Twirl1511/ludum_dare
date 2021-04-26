using UnityEngine.UI;
using UnityEngine;

public class ShowSuckPower : MonoBehaviour
{
    [SerializeField] private Arrow _arrow;
    [SerializeField] private GameObject _suckPowerCanvas;
    [SerializeField] private Text _suckPower;
    [SerializeField] private float _arrowScale = 0.03f;

    //[SerializeField] private Building _building;
    [SerializeField] private Rope _rope;

    private void Update()
    {
        if (_rope.buildingFrom != null && _rope.buildingTo != null)
        {
            //if (_arrow.gameObject.activeSelf == false)
            //{
            //    _arrow.gameObject.SetActive(true);
            //    _arrow.transform.localScale = new Vector3(_arrowScale, _arrowScale, _arrowScale);
            //}
            _rope.CalculateSuckPower();
            //_arrow.SetArrowCount(_rope._currentSuckIndex + 1);
            _suckPowerCanvas.transform.position = _rope._renderer.GetMiddlePos() + Vector3.up * 0.35f;
            //_arrow.transform.position = _rope._renderer.GetMiddlePos();
            //_arrow.transform.rotation = Quaternion.LookRotation((_rope.buildingTo.transform.position - _rope.buildingFrom.transform.position).normalized);
            float deltaHeight = _rope.buildingFrom.transform.position.y - _rope.buildingTo.transform.position.y;
            string gl = "";
            for (int i = 0; i < _rope._currentSuckIndex + 1; i++)
                gl += ">";
            _suckPower.text = $"{gl}";
        }
        else
        {
            if (_suckPowerCanvas.activeSelf)
                _suckPowerCanvas.SetActive(false);
            if (_arrow.gameObject.activeSelf)
                _arrow.gameObject.SetActive(false);
        }
    }
}
