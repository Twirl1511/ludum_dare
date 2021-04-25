using UnityEngine.UI;
using UnityEngine;

public class ShowSuckPower : MonoBehaviour
{
    [SerializeField] private Arrow _arrow;
    [SerializeField] private GameObject _suckPowerCanvas;
    [SerializeField] private Text _suckPower;
    [SerializeField] private float _arrowScale = 0.03f;

    [SerializeField] private Building _building;

    private void Update()
    {
        if (_building.ConnectedBuilding != null)
        {
            if (_arrow.gameObject.activeSelf == false)
            {
                _arrow.gameObject.SetActive(true);
                _arrow.transform.localScale = new Vector3(_arrowScale, _arrowScale, _arrowScale);
            }
            _building.CalculateSuckPower();
            _arrow.SetArrowCount(_building._currentSuckIndex + 1);
            _suckPowerCanvas.transform.position = _building._ropeRender.GetMiddlePos() + Vector3.up * 0.35f;
            _arrow.transform.position = _building._ropeRender.GetMiddlePos();
            _arrow.transform.rotation = Quaternion.LookRotation((_building._ropeRender._p2.position - _building._ropeRender._p1.position).normalized);
            float deltaHeight = _building.ConnectedBuilding.transform.position.y - _building.transform.position.y;
            string gl = "";
            for (int i = 0; i < _building._currentSuckIndex + 1; i++)
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
