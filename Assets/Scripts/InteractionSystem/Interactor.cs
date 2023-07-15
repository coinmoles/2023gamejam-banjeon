using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionRadius;
    [SerializeField] private LayerMask _interactableMask;

    // Start is called before the first frame update
    private void Update()
    {
        Collider2D collider = Physics2D.OverlapCircle(_interactionPoint.position, _interactionRadius, _interactableMask);

        if (collider == null)
            return;

        IInteractable interactable = collider.GetComponentInParent<IInteractable>();

        if (interactable == null)
            return;

        if (Input.GetKeyDown(KeyCode.X))
            interactable.Interact(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionRadius);
    }
}
