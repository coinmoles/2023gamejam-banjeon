using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndManager : MonoBehaviour
{
    public GameEvent OnPlaySFX;

    public void OnGameOver ()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    public void OnGameClear ()
    {
        StartCoroutine(GameClearCoroutine());
    }

    private IEnumerator GameClearCoroutine()
    {
        OnPlaySFX.Raise(this, "game_clear");
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("MainScene");
    }
}
