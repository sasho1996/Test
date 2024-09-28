using System.Collections;
using UnityEngine;

public class ClockModel : MonoBehaviour {

    public bool IsStopTime = false;//Check Is Stop Time
    public float ValueAngleSecondLine=0.0f;//Value Angle Second Line
    public float ValueAngleMinuteLine=0.0f;//Value Angle Minute Line
    public float ValueAngleHourLine=0.0f;//Value Angle Hour Line

    public int ValueSecond=0;//Value Second
    public int ValueMinute=0;//Value Minute
    public int ValueHour=0;//Value Hour

    public float ValueAngleStepSecondLine = 0.0f;//Value Angle Step Second Line
    public float ValueAngleStepMinuteLine = 0.0f;//Value Angle Step Minute Line
    public float ValueAngleStepHourLine = 0.0f;//Value Angle Step Hour Line

    public float OldValue;//Old Value

    public IEnumerator CoroutineWaitAndStartNextCycle;//Coroutine Wait And Start Next Cycle

}
