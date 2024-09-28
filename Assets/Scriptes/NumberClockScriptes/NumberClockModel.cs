using System.Collections;
using UnityEngine;

public class NumberClockModel : MonoBehaviour {

    public int ValueSecond = 0;//Value Second
    public int ValueMinute = 0;//Value Minute
    public int ValueHour = 0;//Value Hour

    public bool IsSetDate = false;//Check Is Set Date 

    public IEnumerator CoroutineGetTimeFromServere;//Coroutine Get Time From Server

    public IEnumerator CoroutineStartClockFromTime;//Coroutine Start Clock From Time

    public IEnumerator CoroutineCheckTimeServer;//Coroutine Check Time Server

}
