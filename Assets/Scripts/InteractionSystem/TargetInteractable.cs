using ScriptableObjectVariable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;

    [SerializeField] private BoolReference _isDay;

    private TargetMurderHandler _targetMurderHandler;

    public string InteractionPrompt => _prompt;

    public bool IsInteractable { get { return !_isDay; } }

    public void Awake()
    {
        _targetMurderHandler = GetComponent<TargetMurderHandler>();
    }

    public bool Interact(Interactor interactor)
    {
        _targetMurderHandler.MurderTried();
        return true;
    }
}
