using System;
using System.Collections;
using UnityEngine;

public class PanelPropertyController : MonoBehaviour {

    [SerializeField] private PanelPropertyView _ppv;
    [SerializeField] private PanelPropertyModel _ppm;


    //Open panel Property
    public void OpenPanelProperty() {

        if (_ppm.IsEnableButton) {

            if (_ppm.IsOpen) {

                _ppm.IsEnableButton = false;

                _ppm.IsOpen = false;

                ChangeValueIsChangeClockLine(false);

                _ppv.HourClock.text = "";
                _ppv.MinuteClock.text = "";
                _ppv.SecondClock.text = "";

                _ppm.HourValue = 0;
                _ppm.MinuteValue = 0;
                _ppm.SecondValue = 0;

                _ppv.ControllerClockLine.ChangeValueIsReady(false);

                _ppv.PanelProperty.SetActive(false);

                _ppv.ControllerNumberClock.RunClock();

                StartCoroutine(ActivateButtonProperty());

            } else {

                _ppm.IsEnableButton = false;

                _ppm.IsOpen = true;

                _ppv.PanelProperty.SetActive(true);

                _ppv.ControllerClockLine.ChangeValueIsReady(true);

                ChangeValueIsChangeClockLine(false);

                StopAllCoroutineInClockAndNumberClock();

                StartCoroutine(ActivateButtonProperty());

            }

        }

    }

    //Stop All Coroutin In Clock And Number Clock
    public void StopAllCoroutineInClockAndNumberClock() {

        _ppv.ControllerClock.StopAllCoroutinesInController();

        _ppv.ControllerNumberClock.StopAllCoroutinesInControllers();

    }

    //Change Date
    public void ChangeDate() {
        
        if (_ppv.HourClock.text != "" && _ppv.MinuteClock.text != "" && _ppv.SecondClock.text != "") {

            ChangeValueIsChangeClockLine(false);

            _ppv.ControllerNumberClock.StopCoroutineGetDateFromServer();

            _ppv.ControllerNumberClock.StopCoroutineStartClockFromTime();

            _ppv.ControllerClock.StopCoroutineWaitAndStartNextCycle();            

            _ppm.HourValue = Convert.ToInt16(_ppv.HourClock.text);

            _ppm.MinuteValue = Convert.ToInt16(_ppv.MinuteClock.text);

            _ppm.SecondValue = Convert.ToInt16(_ppv.SecondClock.text);

            _ppm.HourValue=Math.Abs(_ppm.HourValue);

            _ppm.MinuteValue = Math.Abs(_ppm.MinuteValue);

            _ppm.SecondValue = Math.Abs(_ppm.SecondValue);

            _ppv.ControllerClockLine.ResetObjectRotation();

            if (_ppm.HourValue < 24 && _ppm.MinuteValue < 60 && _ppm.SecondValue < 60) {

                _ppv.SecondClock.text = "";

                _ppv.MinuteClock.text = "";

                _ppv.HourClock.text = "";

                _ppv.ControllerNumberClock.SetDateBySelf(_ppm.HourValue, _ppm.MinuteValue, _ppm.SecondValue);

                _ppv.ControllerClockLine.ChangeValueIsReady(false);

                _ppm.IsOpen = false;

                _ppv.HourClock.text = "";
                _ppv.MinuteClock.text = "";
                _ppv.SecondClock.text = "";

                _ppm.HourValue = 0;
                _ppm.MinuteValue = 0;
                _ppm.SecondValue = 0;

                _ppv.PanelProperty.SetActive(false);

            } else {

                _ppv.SecondClock.text = "";

                _ppv.MinuteClock.text = "";

                _ppv.HourClock.text = "";

            }

        } else {

            _ppv.SecondClock.text = "";

            _ppv.MinuteClock.text = "";

            _ppv.HourClock.text = "";

            if (_ppm.IsChangeClockLine) {
                
                ChangeClockLine();

            }

        }

    }

    //Change Value Is Change Clock Line
    public void ChangeValueIsChangeClockLine(bool isValue) {

        _ppm.IsChangeClockLine = isValue;
        
    }
    //Get Value Is Change Clock
    public bool GetValueIsChangeClock() {
        
        return _ppm.IsChangeClockLine;

    }

    //Change Clock Line
    public void ChangeClockLine() {

        _ppv.ControllerNumberClock.SetDateBySelf(23 - _ppv.ControllerClockLine.GetValueHourLine(), (60 - _ppv.ControllerClockLine.GetValueMinuteLine()), (60 - _ppv.ControllerClockLine.GetValueSecondLine()));

        _ppv.ControllerClockLine.ChangeValueIsReady(false);

        _ppm.IsOpen = false;

        _ppv.HourClock.text = "";
        _ppv.MinuteClock.text = "";
        _ppv.SecondClock.text = "";

        _ppm.HourValue = 0;
        _ppm.MinuteValue = 0;
        _ppm.SecondValue = 0;

        _ppv.PanelProperty.SetActive(false);

    }

    //Activate Button Property
   IEnumerator ActivateButtonProperty() {

        yield return new WaitForSeconds(2.0f);

        _ppm.IsEnableButton = true;

    }

}
