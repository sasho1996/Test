using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

public class NumberClockController : MonoBehaviour {

    [SerializeField]private NumberClockModel _ncm;//Number Clock Model
    [SerializeField] private NumberClockView _ncv;//Number Clock View

    //Run Clock
    public void RunClock() {
        
        if (_ncm.CoroutineGetTimeFromServere == null) {

            _ncm.CoroutineGetTimeFromServere = GetDateTimeFromServer("https://yandex.com/time/sync.json");

        }

        if (_ncm.CoroutineCheckTimeServer != null) {

            StopAllCoroutines();

            _ncm.CoroutineCheckTimeServer = null;

        }

        StartCoroutine(_ncm.CoroutineGetTimeFromServere);

    }

    //Set Date By Self
    public void SetDateBySelf(int hour, int minute, int second) {

        StopAllCoroutines();

        _ncm.CoroutineStartClockFromTime = StartClockFromTime(hour, minute, second);

        StartCoroutine(_ncm.CoroutineStartClockFromTime);

    }

    //Set Date 
    public void SetDate(int hour, int minute, int second) {
        
        _ncm.CoroutineStartClockFromTime = StartClockFromTime(hour, minute, second);        

        StartCoroutine(_ncm.CoroutineStartClockFromTime);        

    }
    
    //Stop Coroutine Get Date From Server
    public void StopCoroutineGetDateFromServer() {

        if( _ncm.CoroutineGetTimeFromServere != null) {
            
            StopCoroutine(_ncm.CoroutineGetTimeFromServere);

            _ncv.NumbersClock[0].GetComponent<SpriteRenderer>().sprite = _ncv.NumbersSprites[0];

            _ncv.NumbersClock[1].GetComponent<SpriteRenderer>().sprite = _ncv.NumbersSprites[0];

            _ncv.NumbersClock[2].GetComponent<SpriteRenderer>().sprite = _ncv.NumbersSprites[0];

            _ncm.IsSetDate = false;

            _ncm.CoroutineGetTimeFromServere = null;

        }

    }
    
    //Stop Coroutine Start Clock From Time
    public void StopCoroutineStartClockFromTime() {

        if (_ncm.CoroutineStartClockFromTime != null) {

            StopCoroutine(_ncm.CoroutineStartClockFromTime);

            _ncv.NumbersClock[0].GetComponent<SpriteRenderer>().sprite = _ncv.NumbersSprites[0];

            _ncv.NumbersClock[1].GetComponent<SpriteRenderer>().sprite = _ncv.NumbersSprites[0];

            _ncv.NumbersClock[2].GetComponent<SpriteRenderer>().sprite = _ncv.NumbersSprites[0];

            _ncm.IsSetDate = false;

            _ncm.CoroutineStartClockFromTime = null;

        }

    }
  
    //Stop All Coroutines In Controllers
    public void StopAllCoroutinesInControllers() {

        StopAllCoroutines();

        _ncm.IsSetDate = false;
        
        _ncm.CoroutineGetTimeFromServere = null;
        
        _ncm.CoroutineStartClockFromTime = null;

        _ncv.NumbersClock[0].GetComponent<SpriteRenderer>().sprite = _ncv.NumbersSprites[0];

        _ncv.NumbersClock[1].GetComponent<SpriteRenderer>().sprite = _ncv.NumbersSprites[0];

        _ncv.NumbersClock[2].GetComponent<SpriteRenderer>().sprite = _ncv.NumbersSprites[0];

    }

    //Set Second Value
    public void SetSecondValue(bool isEnter) {

        if (!isEnter) {

            _ncm.ValueSecond--;

        } else {

            _ncm.ValueSecond++;

        }
        
    }

    //Set Sprite Second Value
    public void SetSpriteSecondValue(int second) {

        _ncv.NumbersClock[2].GetComponent<SpriteRenderer>().sprite = _ncv.NumbersSprites[second];

    }

    //Set Sprite Minute Value
    public void SetSpriteMinuteValue(int minute) {

        _ncv.NumbersClock[1].GetComponent<SpriteRenderer>().sprite = _ncv.NumbersSprites[minute];

    }

    //Set Sprite Hour Value
    public void SetSpriteHourValue(int hour) {

        _ncv.NumbersClock[0].GetComponent<SpriteRenderer>().sprite = _ncv.NumbersHour[hour];

    }

    //Json Date Time Converter
    public DateTime JSONdateTime2dateTime(long JSONdatetimelong) {

        long DT_Tic = (JSONdatetimelong + 62135607600000) * 10000;
        return new DateTime(DT_Tic);

    }

    //Get Date Time From Server
    IEnumerator GetDateTimeFromServer(string uri) {

        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri)) {

            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result) {

                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:

                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);

                    break;

                case UnityWebRequest.Result.ProtocolError:

                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);

                    break;

                case UnityWebRequest.Result.Success:

                    string resultString = Regex.Match(webRequest.downloadHandler.text, @"\d+").Value;

                    DateTime dt = JSONdateTime2dateTime(long.Parse(resultString));

                    if (_ncm.CoroutineCheckTimeServer == null) {

                        _ncm.CoroutineCheckTimeServer = CheckTimeServer();

                        StartCoroutine(_ncm.CoroutineCheckTimeServer);

                    }
                    
                    StartCoroutine(_ncm.CoroutineCheckTimeServer);

                    SetDate(dt.Hour, dt.Minute, dt.Second);                

                    break;

            }

        }

    }

    //Start Clock From Time
    IEnumerator StartClockFromTime(int hour, int minute, int second) {

        _ncv.NumbersClock[0].GetComponent<SpriteRenderer>().sprite = _ncv.NumbersSprites[hour];

        _ncv.NumbersClock[1].GetComponent<SpriteRenderer>().sprite = _ncv.NumbersSprites[minute];

        _ncv.NumbersClock[2].GetComponent<SpriteRenderer>().sprite = _ncv.NumbersSprites[second];

        if (!_ncm.IsSetDate) {

            if (_ncv.ControllerPanelProperty.GetValueIsChangeClock()) {
                
                _ncv.ObjectClockController.SetDateFromLine(hour, minute, second);

            } else {
                
                _ncv.ObjectClockController.SetDate(hour, minute, second);

            }
            _ncm.IsSetDate = true;

        }

        for (; second < 61; second++) {

            _ncv.NumbersClock[2].GetComponent<SpriteRenderer>().sprite = _ncv.NumbersSprites[second];

            yield return new WaitForSeconds(1.0f);

        }

        if (minute != 59) {

            minute++;

            _ncv.NumbersClock[1].GetComponent<SpriteRenderer>().sprite = _ncv.NumbersSprites[minute];

        } else {

            minute = 0;

            _ncv.NumbersClock[1].GetComponent<SpriteRenderer>().sprite = _ncv.NumbersSprites[minute];

            if (hour != 23) {

                hour++;

            } else {

                hour = 0;

            }

            _ncv.NumbersClock[0].GetComponent<SpriteRenderer>().sprite = _ncv.NumbersSprites[hour];

        }

        second = 1;

        _ncv.NumbersClock[2].GetComponent<SpriteRenderer>().sprite = _ncv.NumbersSprites[second];

        _ncm.CoroutineStartClockFromTime = StartClockFromTime(hour, minute, second);

        StartCoroutine(_ncm.CoroutineStartClockFromTime);

    }

    //Check Time Server
    IEnumerator CheckTimeServer() {

        yield return new WaitForSeconds(3600.0f);

        if (_ncm.CoroutineGetTimeFromServere == null) {

            _ncm.CoroutineGetTimeFromServere = GetDateTimeFromServer("https://yandex.com/time/sync.json");
        
        }

        StartCoroutine(_ncm.CoroutineGetTimeFromServere);

    }

    private void Awake() {

        if (_ncm.CoroutineGetTimeFromServere == null) {

            _ncm.CoroutineGetTimeFromServere = GetDateTimeFromServer("https://yandex.com/time/sync.json");

        }

        StartCoroutine(_ncm.CoroutineGetTimeFromServere);

    }

}
