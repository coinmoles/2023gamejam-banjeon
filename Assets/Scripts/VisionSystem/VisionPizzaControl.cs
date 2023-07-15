using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionPizzaControl : MonoBehaviour
{
    private ObjectController _objectController;
    private SpriteRenderer _renderer;
    private Vector3 _initialLocalPosition;

    private void Awake()
    {
        _objectController = GetComponentInParent<ObjectController>();
        _renderer = GetComponentInParent<SpriteRenderer>();
    }

    private void Start ()
    {
        _initialLocalPosition = transform.localPosition;
    }

    private void Update()
    {
        transform.localPosition = new Vector2(_initialLocalPosition.x * (_objectController.FaceLeft ? -1 : 1), _initialLocalPosition.y);
        _renderer.flipX = _objectController.FaceLeft;
    }
}
