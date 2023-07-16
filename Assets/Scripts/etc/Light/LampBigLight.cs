using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampBigLight : MonoBehaviour
{
    UnityEngine.Rendering.Universal.Light2D light;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DayLight()
    {
        light.intensity = 0f;
    }

    public void NightLight()
    {
        light.intensity = 1f;
    }
}
