using ScriptableObjectVariable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkObjectInteractable : MonoBehaviour, IInteractable
{
    private string _prompt = "Break";

    [SerializeField] private BoolReference _isDay;

    private WorkObjectWorkHandler _workObjectWorkHandler;

    public string InteractionPrompt => _prompt;

    public bool IsInteractable { get { return !_isDay; } }

    public void Awake()
    {
        _workObjectWorkHandler = GetComponent<WorkObjectWorkHandler>();
    }

    public bool Interact(Interactor interactor)
    {
        _workObjectWorkHandler.WorkSabotaged();
        return true;
    }
}
