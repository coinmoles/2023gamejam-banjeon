using ScriptableObjectVariable;
using UnityEngine;

public class NpcVision : MonoBehaviour
{
    [SerializeField] private FloatReference _detectionDistance;

    [SerializeField] private BoolReference _isDay;

    [SerializeField] private LayerMask _playerLayer;

    [SerializeField] private GameEvent _onPlayerHit;
    [SerializeField] private GameEvent _onDayStart;

    private ObjectController _objectController;
    private NpcAI _npcAI;
    private NpcSnack _npcSnack;

    private void Awake()
    {
        _objectController = GetComponent<ObjectController>();
        _npcAI = GetComponent<NpcAI>();
        _npcSnack = GetComponent<NpcSnack>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (_isDay)
            return;
        if (_npcAI.GoState == GoState.OnLowerLevelLadder || _npcAI.GoState == GoState.OnHigherLevelLadder)
            return;
        else if (_npcSnack.IsAggro)
            return;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, _objectController.FaceLeft ? Vector2.left : Vector2.right, _detectionDistance, _playerLayer);
        
        if (hit.collider != null)
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                Debug.Log("Collider");
                _onPlayerHit.Raise(this, null);
                _onDayStart.Raise(this, null);
            }
    }

    private void OnDrawGizmos()
    {
        if (!_isDay)
            if (_objectController != null)
                Gizmos.DrawLine(transform.position, (Vector2)transform.position + (_objectController.FaceLeft ? Vector2.left : Vector2.right) * _detectionDistance);
    }
}
