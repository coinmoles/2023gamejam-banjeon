using ScriptableObjectVariable;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [Header("Interaction Config")]
    [SerializeField] private Vector2 _interactionPointOffset;
    [SerializeField] private float _interactionRadius;
    [SerializeField] private LayerMask _dayInteractableMask;
    [SerializeField] private LayerMask _nightInteractableMask;

    [Header("Game State")]
    [SerializeField] private BoolReference _isDay;

    private PlayerController _playerController;
    private PlayerPromptController _playerPromptController;

    public LayerMask InteractableMask { get { return _isDay ? _dayInteractableMask : _nightInteractableMask; } }

    private void Awake()
    {
        _playerController = GetComponentInParent<PlayerController>();
        _playerPromptController = GetComponentInParent<PlayerPromptController>();
    }
    
    // Start is called before the first frame update
    private void Update()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, _interactionRadius, InteractableMask);

        if (collider == null)
        {
            _playerPromptController.SetShow(false);
            return;
        }

        IInteractable interactable = collider.GetComponentInParent<IInteractable>();

        if (interactable == null)
        {
            _playerPromptController.SetShow(false);
            return;
        }

        Debug.Log(interactable.InteractionPrompt);

        _playerPromptController.OnInteractShow(interactable.InteractionPrompt);
        _playerPromptController.SetShow(true);

        if (!interactable.IsInteractable)
        {
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.X))
                interactable.Interact(this);
        }
    }

    private void FixedUpdate()
    {
        transform.localPosition = _interactionPointOffset * (_playerController.FaceLeft ? - 1 : 1);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _interactionRadius);
    }
}
