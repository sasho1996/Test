using DG.Tweening;
using System.Collections;
using UnityEngine;

public class ClockController : MonoBehaviour {

    [SerializeField] private ClockModel _cm;//Clock Model
    [SerializeField] private ClockView _cv;//Clock View

    //Init Data
    public void Init() {

        _cm.ValueAngleStepSecondLine = 6.0f;

        _cm.ValueAngleStepMinuteLine = 6.0f / 60.0f;

        _cm.ValueAngleStepHourLine = 6.0f / 3600.0f;
        

        UpdateTime();

    }

    //Set Date By Self
    public void SetDateBySelf(int hour, int minute, int second) {

        SetDate(hour, minute, second);

    }

    //Update Time
    public void UpdateTime() {

        _cm.ValueHour = System.DateTime.Now.Hour;

        _cm.ValueMinute = System.DateTime.Now.Minute;

        _cm.ValueSecond = System.DateTime.Now.Second;

    }

    //Run Clock
    public void RunClock() {

        _cv.ObjectSecondLine.transform.DORotateQuaternion(Quaternion.Euler(0.0f, 0.0f, _cv.ObjectSecondLine.transform.eulerAngles.z - _cm.ValueAngleStepSecondLine), 1.0f);
        _cv.ObjectMinuteLine.transform.DORotateQuaternion(Quaternion.Euler(0.0f, 0.0f, _cv.ObjectMinuteLine.transform.eulerAngles.z - _cm.ValueAngleStepMinuteLine), 1.0f);
        _cv.ObjectHourLine.transform.DORotateQuaternion(Quaternion.Euler(0.0f, 0.0f, _cv.ObjectHourLine.transform.eulerAngles.z - _cm.ValueAngleStepHourLine), 1.0f);

        StartCoroutine(WaitAndStartNextCycle());

    }

    public int firstNumber = 0;
    public int secondNumber = 0;
    public float checkhour = 0.0f;


    public float OldValue = 0.0f;

    public float diffSecond = 0.0f;
    public float diffMinute = 0.0f;
    public float diffHour = 0.0f;

    public float AngleMinuteLine = 0.0f;
    public float AngleHourLine = 0.0f;

    public int second = 0;
    public float oldMinute = 0.0f;
    //Set Date By Self
    public void SetDateBySelf(float angleSecondLine, float nowAngleMinuteLine, float nowAngleHourLine, GameObject objMinuteLine, GameObject objHourLine, GameObject objSecondLine) {

        if (angleSecondLine != OldValue) {
            var m = 60 - (objSecondLine.transform.eulerAngles.z / 6.0f);
            m /= 60;
      

            if (angleSecondLine > OldValue) {
                
                objMinuteLine.transform.eulerAngles = new Vector3(0, 0, -((m * 6.0f)));
                //objMinuteLine.transform.eulerAngles = new Vector3(0, 0, nowAngleMinuteLine + (diffMinute/6.0f));

            } else {

                objMinuteLine.transform.eulerAngles = new Vector3(0, 0, ((m * 6.0f)));
                //objMinuteLine.transform.eulerAngles = new Vector3(0, 0, nowAngleMinuteLine - (diffMinute/6.0f));

            }
           
            _cv.ControllerNumberClock.SetSpriteSecondValue((int)(60 - (angleSecondLine / 6.0f)));

            _cv.ControllerNumberClock.SetSpriteMinuteValue((int)(60 - (nowAngleMinuteLine / 6.0f)));

        }

        OldValue = angleSecondLine;
       
    }

    //Set Date From Line
    public void SetDateFromLine(int hour, int minute, int second) {

        _cm.CoroutineWaitAndStartNextCycle = WaitAndStartNextCycle();

        StartCoroutine(_cm.CoroutineWaitAndStartNextCycle);

    }

    //Set Date
    public void SetDate(int hour, int minute, int second) {
        
        _cm.ValueAngleSecondLine = second * 6.0f;        

        _cm.ValueAngleMinuteLine = minute* 6.0f;        

        _cm.ValueAngleMinuteLine += (_cm.ValueAngleStepMinuteLine * second);

        _cm.ValueAngleHourLine = hour * 31f;

        _cm.ValueAngleHourLine += (_cm.ValueAngleStepHourLine * minute);
        

        _cv.ObjectSecondLine.transform.DORotateQuaternion(Quaternion.Euler(0.0f, 0.0f, 0.0f), 0f);        
        _cv.ObjectMinuteLine.transform.DORotateQuaternion(Quaternion.Euler(0.0f, 0.0f, 0.0f), 0f);
        _cv.ObjectHourLine.transform.DORotateQuaternion(Quaternion.Euler(0.0f, 0.0f, 0.0f), 0f);

        _cv.ObjectSecondLine.transform.DORotateQuaternion(Quaternion.Euler(0.0f, 0.0f, _cv.ObjectSecondLine.transform.eulerAngles.z - _cm.ValueAngleSecondLine), 0f);
        _cv.ObjectMinuteLine.transform.DORotateQuaternion(Quaternion.Euler(0.0f, 0.0f, _cv.ObjectMinuteLine.transform.eulerAngles.z - _cm.ValueAngleMinuteLine), 0f);
        _cv.ObjectHourLine.transform.DORotateQuaternion(Quaternion.Euler(0.0f, 0.0f, _cv.ObjectHourLine.transform.eulerAngles.z - _cm.ValueAngleHourLine), 0f);
        
        _cm.CoroutineWaitAndStartNextCycle = WaitAndStartNextCycle();
        
        StartCoroutine(_cm.CoroutineWaitAndStartNextCycle);

    }

    //Stop Coroutine Wait And Start Next Cycle
    public void StopCoroutineWaitAndStartNextCycle() {
        
        StopAllCoroutines();        

        _cm.IsStopTime = true;
                  

    }

    IEnumerator ResetPosition() {

        yield return new WaitForSeconds(1.0f);

        _cv.ObjectSecondLine.transform.DORotateQuaternion(Quaternion.Euler(0.0f, 0.0f, 0.0f), 0.0f);
        _cv.ObjectMinuteLine.transform.DORotateQuaternion(Quaternion.Euler(0.0f, 0.0f, 0.0f), 0.0f);
        _cv.ObjectHourLine.transform.DORotateQuaternion(Quaternion.Euler(0.0f, 0.0f, 0.0f), 0.0f);

    }

    //Wait And Start Next Cycle
    IEnumerator WaitAndStartNextCycle() {

        yield return new WaitForSeconds(1.0f);

        RunClock();

    }

    //Stop All Coroutines In Controller
    public void StopAllCoroutinesInController() {

        StopAllCoroutines();

        StartCoroutine(ResetPosition());

        _cm.CoroutineWaitAndStartNextCycle = null;

    }

    private void Awake() {

        Init();      

    }

}
