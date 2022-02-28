using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SunLight : MonoBehaviour
{

    private Light2D l;

    public float secondsInFullDay = 12f;
    [Range(0, 1)]
    public float currentTimeOfDay = 0;


    [Range(0.6f, 0.8f)]
    public float evening = 0.73f;
    [Range(0.1f, 0.3f)]
    public float morning = 0.23f;
    [Range(0, 0.1f)]
    public float transitionTime = 0.02f;

    [HideInInspector]
    public float timeMultiplier = 1f;

    [Range(0, 1)]
    public float minBrightness = 0.2f;

    [Range(0, 1)]
    public float maxBrightness = 1f;

    public bool isDay;

    public int hour, minute;

    public string clock;

    float sunInitialIntensity;

    // Start is called before the first frame update
    void Start()
    {
        l = GetComponent<Light2D>();
        sunInitialIntensity = l.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSun();

        currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;

        if (currentTimeOfDay >= 1)
        {
            currentTimeOfDay = 0;
        }
    }

    void UpdateSun()
    {
        //sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90, 170, 0);

        float intensityMultiplier = maxBrightness;
        if (currentTimeOfDay <= morning || currentTimeOfDay >= evening+transitionTime)
        {
            intensityMultiplier = minBrightness;
        }
        else if (currentTimeOfDay <= morning+transitionTime)
        {
            intensityMultiplier = Mathf.Clamp((currentTimeOfDay - morning) * (1 / transitionTime),minBrightness,maxBrightness);
        }
        else if (currentTimeOfDay >= evening)
        {
            intensityMultiplier = Mathf.Clamp(1 - ((currentTimeOfDay - evening) * (1 / transitionTime)),minBrightness,maxBrightness);
        }
        if(currentTimeOfDay >= morning && currentTimeOfDay <= evening)
        {
            isDay = true;
        }
        else
        {
            isDay = false;
        }
        l.intensity = sunInitialIntensity * intensityMultiplier;
        float valueOfHour = Remap(currentTimeOfDay,0, 1, 0, 24);
        hour = (int)valueOfHour;
        minute = (int)Remap(valueOfHour - (float)hour, 0, 1, 0, 60);
        string stringHour = hour.ToString();
        string stringMinute = minute.ToString();

        if(hour < 10)
        {
            stringHour = "0" + stringHour;
        }
        if(minute < 10)
        {
            stringMinute = "0" + stringMinute;
        }
        //Debug.Log("Time: "+ currentTimeOfDay+"  ==>   "+ stringHour + ":" + stringMinute);
        //Debug.Log(currentTimeOfDay.ToString() + " corresponds to " + valueOfHour.ToString());
        clock = stringHour + ":" + stringMinute;
    }

    private float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
