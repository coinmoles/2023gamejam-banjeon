using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
using TMPro;

public class StartButton : MonoBehaviour
{
    [SerializeField] private GameEvent _onPlaySFX;
    [SerializeField] private Light2D light;
    [SerializeField] private TMP_Text text;
    private bool touched = false;
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
        if (touched)
        {
            return;
        }
        touched = true;
        _onPlaySFX.Raise(this, "wolf_howling");
        StartCoroutine(StartGameCo());
    }

    private IEnumerator StartGameCo()
    {
        float elapsedTime = 0f;
        while (elapsedTime < 2f)
        {
            elapsedTime += Time.deltaTime;
            light.intensity = light.intensity / 1.03f;
            float H, S, V;
            Color.RGBToHSV(text.color, out H, out S, out V);
            text.color = Color.HSVToRGB(H, S, V + 0.003f);
            yield return null;
        }
        SceneManager.LoadScene("MainScene");
    }
}
