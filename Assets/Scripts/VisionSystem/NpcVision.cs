using ScriptableObjectVariable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class NpcVision : MonoBehaviour
{
    [SerializeField] private FloatReference _detectionDistance;

    [SerializeField] private GameEvent _onPlayerHit;

    private ObjectController _objectController;

    private void Awake()
    {
        _objectController = GetComponent<ObjectController>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _objectController.FaceLeft ? Vector2.left : Vector2.right, _detectionDistance);

        if (hit.collider != null)
            if (hit.collider.gameObject.tag == "Player")
                _onPlayerHit.Raise(this, null);
    }

    private void OnDrawGizmos()
    {
        if (_objectController != null)
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + (_objectController.FaceLeft ? Vector2.left : Vector2.right) * _detectionDistance);
    }
}
