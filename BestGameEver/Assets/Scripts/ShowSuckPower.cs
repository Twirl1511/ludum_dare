using UnityEngine.UI;
using UnityEngine;

public class ShowSuckPower : MonoBehaviour
{
    [SerializeField] private GameObject _suckPowerCanvas;
    [SerializeField] private Text _suckPower;

    [SerializeField] private Building _building;

    private void Update()
    {
        if (_building.ConnectedBuilding != null)
        {
            if (_suckPowerCanvas.activeSelf == false)
                _suckPowerCanvas.SetActive(true);
            _building.CalculateSuckPower();
            _suckPowerCanvas.transform.position = _building._ropeRender.GetMiddlePos();
            float deltaHeight = _building.ConnectedBuilding.transform.position.y - _building.transform.position.y;
            //_suckPower.text = $"{deltaHeight.ToString("0.00")}";
            //_suckPower.text = $"{deltaHeight.ToString("0.00")} \n {_building._pipeSuckPower.ToString("0.0")}";
            _suckPower.text = $"{_building._pipeSuckPower.ToString("0.0")}";
        }
        else
        {
            if (_suckPowerCanvas.activeSelf)
                _suckPowerCanvas.SetActive(false);
        }
    }
}
