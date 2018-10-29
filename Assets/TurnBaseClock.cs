using UnityEngine;
using System.Collections;

public class TurnBaseClock : Singleton<TurnBaseClock>
{

    //-- set start time 00:00
    public int minutes = 0;
    public int hour = 0;
    public int seconds = 0;
    
    public GameObject pointerMinutes;
    public GameObject pointerHours;

    //-- time speed factor
    public float clockSpeed = 10.0f;     // 1.0f = realtime, < 1.0f = slower, > 1.0f = faster

    //-- internal vars
    float msecs = 0;

    void Start()
    {
    }

    public void UpdateTime(int time = 10)
    {
        //-- calculate time
        minutes += time;
        if (msecs >= 1.0f)
        {
            msecs -= 1.0f;
            seconds++;
            if (seconds >= 60)
            {
                seconds = 0;
                minutes++;
                if (minutes > 60)
                {
                    minutes = 0;
                    hour++;
                    if (hour >= 24)
                        hour = 0;
                }
            }
        }


        //-- calculate pointer angles
        float rotationMinutes = (360.0f / 60.0f) * minutes;
        float rotationHours = ((360.0f / 12.0f) * hour) + ((360.0f / (60.0f * 12.0f)) * minutes);

        //-- draw pointers
        pointerMinutes.transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationMinutes);
        pointerHours.transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationHours);

    }
}
