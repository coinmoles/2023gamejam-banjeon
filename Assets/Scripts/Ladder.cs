using ScriptableObjectVariable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] private FloatReference _ladderX;

    private void Start()
    {
        _ladderX.SetValue(transform.position.x);
    }
}
