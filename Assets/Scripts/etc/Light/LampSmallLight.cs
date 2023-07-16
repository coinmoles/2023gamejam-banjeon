using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampSmallLight : MonoBehaviour
{
    UnityEngine.Rendering.Universal.Light2D light;

    // Start is called before the first frame update
    void Awake()
    {
        light = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
    }

    public void DayLight()
    {
        light.intensity = 0f;
        StopAllCoroutines();
    }

    public void NightLight()
    {
        light.intensity = 1f;
        StartCoroutine(flicking());
    }

    private IEnumerator flicking()
    {
        while (true)
        {
            yield return null;
            light.intensity = 0.5f + Mathf.PingPong(Time.time, 2.5f);
        }
    }
}
