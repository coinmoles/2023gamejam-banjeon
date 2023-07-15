using ScriptableObjectVariable;
using UnityEngine;

public class VisionPizzaControl : MonoBehaviour
{
    [SerializeField] private FloatReference _pizzaDetectionRadius;

    private ObjectController _objectController;
    private SpriteRenderer _renderer;
    private Vector3 _initialLocalPosition;
    private Vector3 _initialLocalScale;
    private GameObject _player;
    private NpcAI _npcAI;

    private void Awake()
    {
        _objectController = GetComponentInParent<ObjectController>();
        _renderer = GetComponent<SpriteRenderer>();
        _npcAI = GetComponentInParent<NpcAI>();
    }

    private void Start ()
    {
        _initialLocalPosition = transform.localPosition;
        _initialLocalScale = transform.localScale;
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (_player != null)
        {
            if ((_player.transform.position - transform.parent.position).magnitude > _pizzaDetectionRadius)
                transform.localScale = Vector3.zero;
            else
                transform.localScale = _initialLocalScale;
        }
        if (_npcAI.GoState == GoState.OnLowerLevelLadder || _npcAI.GoState == GoState.OnHigherLevelLadder)
        {
            transform.localScale = Vector3.zero;
        }
        else
            transform.localScale = _initialLocalScale;

        transform.localPosition = new Vector2(_initialLocalPosition.x * (_objectController.FaceLeft ? 1 : -1), _initialLocalPosition.y);
        _renderer.flipX = !_objectController.FaceLeft;
    }
}
