using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace ScriptableObjectVariable
{
    [CreateAssetMenu(menuName = "Variable/Resetting Float Variable")]
    public class ResettingFloatVariable : FloatVariable, IResettable
    {
        [SerializeField] private float _initialValue;
        [SerializeField] private ResettableRuntimeSet _resettableRuntimeSet;
        public ResettableRuntimeSet ResettableRuntimeSet => _resettableRuntimeSet;

        public void Reset()
        {
            SetValue(_initialValue);
        }

        public void OnDestroy()
        {
            _resettableRuntimeSet.Remove(this);
        }
    }
}