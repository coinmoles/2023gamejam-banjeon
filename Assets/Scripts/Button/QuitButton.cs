using UnityEngine;

public class QuitButton : MonoBehaviour
{
    private bool _touched;
    public void OnClick()
    {
        if (_touched)
        {
            return;
        }
        _touched = true;
        Application.Quit();
    }

}
