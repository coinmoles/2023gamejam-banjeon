using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class StartButton : MonoBehaviour
{
    [SerializeField] private GameEvent _onPlaySFX;
    [SerializeField] private Light2D light;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickStartGame()
    {
        _onPlaySFX.Raise(this, "wolf_howling");
        StartCoroutine(startGameCo());
    }

    private IEnumerator startGameCo()
    {
        float elapsedTime = 0f;
        while (elapsedTime < 2f)
        {
            elapsedTime += Time.deltaTime;
            light.intensity = light.intensity / 1.03f;
            yield return null;
        }
    }
}
