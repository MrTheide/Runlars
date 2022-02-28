using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FireScript : MonoBehaviour
{
    private Light2D l;
    private float innerRadius, intensity;
    public float changeIR, changeI;
    public float cooldown;
    private float timeToGo;
    public Light2D globalLight;

    // Start is called before the first frame update
    void Start()
    {
        l = GetComponent<Light2D>();
        innerRadius = l.pointLightInnerRadius;
        intensity = l.intensity;
        timeToGo = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.fixedTime >= timeToGo)
        {
            l.pointLightInnerRadius = Random.Range(innerRadius + changeIR, innerRadius - changeIR);
            l.intensity = Random.Range(intensity + changeI, intensity - changeI);
            if(null != globalLight)
            {
                l.intensity = l.intensity / (1+ globalLight.intensity);
                /*
                if(l.intensity > globalLight.intensity)
                {
                    l.intensity = globalLight.intensity;
                }
                */
            }
            timeToGo = Time.fixedTime + cooldown;
        }
        
    }
}
