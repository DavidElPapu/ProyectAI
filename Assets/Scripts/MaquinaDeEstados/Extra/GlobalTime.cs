using UnityEngine;
using System.Collections;

public class GlobalTime : MonoBehaviour
{
    [SerializeField] private int hours, minutes;

    [SerializeField]
    private float minuteToRealTime;


    private void Awake()
    {
        hours = 0;
        minutes = 0;
    }

    private void Start()
    {
        OnTimeStart();
    }

    public void OnTimeStart()
    {
        StartCoroutine(TimeActive());
    }

    private IEnumerator TimeActive()
    {
        while (true)
        {
            yield return new WaitForSeconds(minuteToRealTime);
            OnMinute();
        }
    }

    private void OnHour()
    {
        hours++;
        minutes = 0;
        if (hours == 25)
            hours = 0;
    }

    private void OnMinute()
    {
        minutes++;
        if (minutes == 60)
            OnHour();
    }

    public int GetHour()
    {
        return hours;
    }

    public int GetMinutes()
    {
        return minutes;
    }
}
