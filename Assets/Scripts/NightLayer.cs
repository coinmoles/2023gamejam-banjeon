using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightLayer : MonoBehaviour
{
    public void OnDayStart()
    {
        gameObject.SetActive(false);
    }

    public void OnNightStart()
    {
        gameObject.SetActive(true);
    }
}
