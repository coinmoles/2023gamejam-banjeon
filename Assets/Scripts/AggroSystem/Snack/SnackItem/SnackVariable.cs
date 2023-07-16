using UnityEngine;
using System.Security.Cryptography;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu()]
public class SnackVariable : ScriptableObject, IResettable
{
    private SnackSO _snack;

    public SnackSO Snack => _snack;

    [SerializeField] private ResettableRuntimeSet _resettableRuntimeSet;
    public ResettableRuntimeSet ResettableRuntimeSet => _resettableRuntimeSet;

    public void ObtainSnack(SnackSO snack)
    {
        _snack = snack;
    }

    public void UseSnack()
    {
        _snack = null;
    }
    
    public void Reset()
    {
        _snack = null;
    }

    public void Awake()
    {
        _resettableRuntimeSet.Add(this);
    }

    public void OnDestroy()
    {
        _resettableRuntimeSet.Remove(this);
    }
}
