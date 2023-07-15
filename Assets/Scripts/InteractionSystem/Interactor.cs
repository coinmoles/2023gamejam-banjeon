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

    public LayerMask InteractableMask { get { return _isDay ? _dayInteractableMask : _nightInteractableMask; } }

    private void Awake()
    {
        _playerController = GetComponentInParent<PlayerController>();
    }

    // Start is called before the first frame update
    private void Update()
    {
        transform.localPosition = _interactionPointOffset * (_playerController.FaceLeft ? -1 : 1);
        Collider2D collider = Physics2D.OverlapCircle(transform.position, _interactionRadius, InteractableMask);

        if (collider == null)
            return;

        IInteractable interactable = collider.GetComponentInParent<IInteractable>();

        if (interactable == null)
            return;


        if (!interactable.IsInteractable)
        {
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.X))
                interactable.Interact(this);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _interactionRadius);
    }
}
