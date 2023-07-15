using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MainObject : MonoBehaviour
{
    [SerializeField] private GameEvent _onPlaySFX;
    [SerializeField] private Light2D light;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Flick());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Flick()
    {
        light.intensity = 0.3f;
        yield return new WaitForSeconds(0.15f);
        _onPlaySFX.Raise(this, "door_open");
        light.intensity = 0.73f;
        yield return new WaitForSeconds(Random.Range(8f, 15f));
        StartCoroutine(Flick());
    }
}
