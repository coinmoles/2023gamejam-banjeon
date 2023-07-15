using ScriptableObjectVariable;
using UnityEngine;

public class NpcInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    
    [SerializeField] private SnackVariable _snackVariable;
    [SerializeField] private BoolReference _isDay;

    private NpcSnack _npcSnack;

    public string InteractionPrompt => _prompt;

    public bool IsInteractable { get { return _npcSnack.IsLikedSnack(_snackVariable.Snack) && _isDay; } }

    public void Awake()
    {
        _npcSnack = GetComponent<NpcSnack>();
    }

    public bool Interact(Interactor interactor)
    {
        if (_npcSnack.GivenSnack(_snackVariable.Snack))
        {
            _snackVariable.UseSnack();
            return true;
        }
        return false;
    }
}
