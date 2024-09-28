using UnityEngine;

public class ClockLineController : MonoBehaviour {

    [SerializeField] private ClockLineModel _clm;//Clock Line Model
    [SerializeField] private ClockLineView _clv;//Clock Line View
    
    //Init
    public void Init() {

        _clv.MainCamera = Camera.main;

        _clm.SecondLineColliderSizeX=_clv.SecondLineCollider.size.x;

        _clm.SecondLineColliderSizeY=_clv.SecondLineCollider.size.y;

    }

    private void Update() {

        if (_clm.IsReady) {

            _clm.MousePosition = _clv.MainCamera.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButtonDown(0)) {

                if (_clv.SecondLineCollider == Physics2D.OverlapPoint(_clm.MousePosition)) {
                    
                    _clv.SecondLineCollider.size = new Vector2(1000.0f, 1000.0f);

                    _clm.ScreenPosition = _clv.MainCamera.WorldToScreenPoint(transform.position);

                    _clm.DifferenceMousePositionAndScreenPosition = Input.mousePosition - _clm.ScreenPosition;

                    _clm.SecondAngleOffset = (Mathf.Atan2(_clv.ObjectSecondLine.transform.right.y, _clv.ObjectSecondLine.transform.right.x) - Mathf.Atan2(_clm.DifferenceMousePositionAndScreenPosition.y, _clm.DifferenceMousePositionAndScreenPosition.x)) * Mathf.Rad2Deg;
                    _clm.MinuteAngleOffset = (Mathf.Atan2(_clv.ObjectMinuteLine.transform.right.y, _clv.ObjectMinuteLine.transform.right.x) - Mathf.Atan2(_clm.DifferenceMousePositionAndScreenPosition.y, _clm.DifferenceMousePositionAndScreenPosition.x)) * Mathf.Rad2Deg;
             
                }

            }

            if (Input.GetMouseButton(0)) {

                if (_clv.SecondLineCollider == Physics2D.OverlapPoint(_clm.MousePosition)) {

                    _clm.DifferenceMousePositionAndScreenPosition = Input.mousePosition - _clm.ScreenPosition;

                    _clm.AngleSecondLine = Mathf.Atan2(_clm.DifferenceMousePositionAndScreenPosition.y, _clm.DifferenceMousePositionAndScreenPosition.x) * Mathf.Rad2Deg;

                    _clm.AngleMinuteLine = Mathf.Atan2(_clm.DifferenceMousePositionAndScreenPosition.y, _clm.DifferenceMousePositionAndScreenPosition.x) * Mathf.Rad2Deg;

                    _clm.AngleHourLine = Mathf.Atan2(_clm.DifferenceMousePositionAndScreenPosition.y, _clm.DifferenceMousePositionAndScreenPosition.x) * Mathf.Rad2Deg;

                    _clv.ObjectSecondLine.transform.eulerAngles = new Vector3(0, 0, _clm.AngleSecondLine + _clm.SecondAngleOffset);

                    var s = _clv.ObjectSecondLine.transform.eulerAngles.z / 6.0f;

                    s = 60.0f - s;

                    s += 0.1f;

                    var m = s / 60.0f;

                    var h = m / 60.0f;

                    if (_clm.OldValue != _clv.ObjectSecondLine.transform.eulerAngles.z) {

                        if (!_clv.ControllerPanelProperty.GetValueIsChangeClock()) {
                            
                            _clv.ControllerPanelProperty.ChangeValueIsChangeClockLine(true);
                        
                        }
                        if (_clm.OldValue > _clv.ObjectSecondLine.transform.eulerAngles.z) {

                            _clv.ObjectMinuteLine.transform.eulerAngles = new Vector3(0, 0, _clv.ObjectMinuteLine.transform.eulerAngles.z - ((m * 6.0f) / 60.0f));
                            _clv.ObjectHourLine.transform.eulerAngles = new Vector3(0, 0, _clv.ObjectHourLine.transform.eulerAngles.z - ((h * 30.0f) / 60.0f));

                        } else {

                            _clv.ObjectMinuteLine.transform.eulerAngles = new Vector3(0, 0, _clv.ObjectMinuteLine.transform.eulerAngles.z + ((m * 6.0f) / 60.0f));
                            _clv.ObjectHourLine.transform.eulerAngles = new Vector3(0, 0, _clv.ObjectHourLine.transform.eulerAngles.z + ((h * 30.0f) / 60.0f));

                        }

                    }

                }

            }

            if (Input.GetMouseButtonUp(0)) {

                _clv.SecondLineCollider.size = new Vector2(_clm.SecondLineColliderSizeX, _clm.SecondLineColliderSizeY);

            }

            _clm.OldValue = _clv.ObjectSecondLine.transform.eulerAngles.z;
      
        }

    }    
    
    //Reset Object Rotation
    public void ResetObjectRotation() {

        _clv.ObjectHourLine.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        _clv.ObjectMinuteLine.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        _clv.ObjectSecondLine.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);

    }

    //Get Value Second Line
    public int GetValueSecondLine() {

        return (int)(_clv.ObjectSecondLine.transform.eulerAngles.z / 6.0f);

    }

    //Get Value Minute Line
    public int GetValueMinuteLine() {

        return (int)((_clv.ObjectMinuteLine.transform.eulerAngles.z / 6.0f));

    }


    //Get Value Hour Line
    public int GetValueHourLine() {

        return (int)(_clv.ObjectHourLine.transform.eulerAngles.z / 30.0f);

    }

    //Change Value Is Ready
    public void ChangeValueIsReady(bool isReady) {

        _clm.IsReady = isReady;

    }

    public void Start() {

        Init();

    }

}
