using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager tM;
    public float SlowMotionFactor = 0.05f;

    private void Start()
    {
        tM = this;
    }
    public void slowMo()
    {
        Time.timeScale = SlowMotionFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
    public void norMotion()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}
