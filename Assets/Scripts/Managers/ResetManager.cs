using ScriptableObjectVariable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetManager : MonoBehaviour
{
    [SerializeField] private ResettableRuntimeSet resettableRuntimeSet;

    // Start is called before the first frame update
    void Start()
    {
        resettableRuntimeSet.ResetAll();
    }
}
