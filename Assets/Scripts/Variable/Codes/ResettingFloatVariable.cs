using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace ScriptableObjectVariable
{
    [CreateAssetMenu(menuName = "Variable/Resetting Float Variable")]
    public class ResettingFloatVariable : FloatVariable
    {
        private float _initialValue;

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
                case PlayModeStateChange.EnteredPlayMode:
                    _initialValue = Value;
                    break;

                case PlayModeStateChange.ExitingPlayMode:
                    EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
                    SetValue(_initialValue);
                    break;
            }
        }
#endif
    }
}