using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityLight : MonoBehaviour
{
    UnityEngine.Rendering.Universal.Light2D light;

    // Start is called before the first frame update
    void Awake()
    {
        light = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
    }

    public void dayLight()
    {
        light.intensity = 0f;
    }

    public void nightLight()
    {
        light.intensity = 0.8f;
    }
}
