using ScriptableObjectVariable;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [Header("Interaction Config")]
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionRadius;
    [SerializeField] private LayerMask _dayInteractableMask;
    [SerializeField] private LayerMask _nightInteractableMask;

    [Header("Game State")]
    [SerializeField] private BoolReference _isDay;

    public LayerMask InteractableMask { get { return _isDay ? _dayInteractableMask : _nightInteractableMask; } }

    // Start is called before the first frame update
    private void Update()
    {
        Collider2D collider = Physics2D.OverlapCircle(_interactionPoint.position, _interactionRadius, InteractableMask);

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
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionRadius);
    }
}
