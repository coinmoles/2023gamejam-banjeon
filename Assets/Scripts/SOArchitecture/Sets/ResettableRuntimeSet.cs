using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Runtime Set/Resettable Runtime Set")]
public class ResettableRuntimeSet : RuntimeSet<ScriptableObject>
{
    public void ResetAll()
    {
        foreach(IResettable resettable in Items)
        {
            resettable.Reset();
        }
    }
}

public interface IResettable
{
    public void Reset();
}
