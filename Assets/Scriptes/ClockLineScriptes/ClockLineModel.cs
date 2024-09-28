using UnityEngine;

public class ClockLineModel : MonoBehaviour {

    public float SecondLineColliderSizeX = 0.0f;//Second Line Collider Size X

    public float SecondLineColliderSizeY = 0.0f;//Second Line Collider Sise Y

    public float SecondAngleOffset;//Angle Offset
    public float MinuteAngleOffset;//Angle Offset

    public float AngleSecondLine;//Angle Second Line
    public float AngleMinuteLine;//Angle Minute Line
    public float AngleHourLine;//Angle Hour Line

    public float OldValue = 0.0f;//Old Value

    public bool IsReady = false;//Check Is Ready

    public Vector3 ScreenPosition;//Screen Position;

    public Vector3 MousePosition;//Mouse Position

    public Vector3 DifferenceMousePositionAndScreenPosition;//Difference Mouse Position And Screen Position

}
