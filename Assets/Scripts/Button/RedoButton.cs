using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RedoButton : MonoBehaviour
{
    [SerializeField] private GameEvent _onPlaySFX;
    [SerializeField] private TMP_Text text;
    private bool _touched = false;

    public void OnClick()
    {
        if (_touched)
        {
            return;
        }
        _touched = true;
        _onPlaySFX.Raise(this, "wolf_howling");
        StartCoroutine(StartGameCo());
    }

    private IEnumerator StartGameCo()
    {
        float elapsedTime = 0f;
        while (elapsedTime < 2f)
        {
            elapsedTime += Time.deltaTime;
            float H, S, V;
            Color.RGBToHSV(text.color, out H, out S, out V);
            text.color = Color.HSVToRGB(H, S, V + 0.003f);
            yield return null;
        }
        SceneManager.LoadScene("MainScene");
    }
}
