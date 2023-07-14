using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu()]
public class SnackVariable : ScriptableObject
{
    private SnackSO _snack;

    public SnackSO Snack => _snack;

    public void ObtainSnack(SnackSO snack)
    {
        _snack = snack;
    }

    public void UseSnack()
    {
        _snack = null;
    }

    private void OnEnable()
    {
#if UNITY_EDITOR
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
#endif
    }

#if UNITY_EDITOR
    private void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        switch (state)
        {
            case PlayModeStateChange.ExitingPlayMode:
                EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
                _snack = null;
                break;
        }
    }
#endif
}
