using ScriptableObjectVariable;
using UnityEngine;

public class SnackItemMovement : MonoBehaviour
{
    [SerializeField] private Vector3 _initialPosition;
    [SerializeField] private FloatReference _snackItemAmplitude;
    [SerializeField] private FloatReference _snackItemPeriod;

    private float _startTime;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        _startTime = Time.time;
        _initialPosition = transform.position;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float timeDiff = Time.time - _startTime;
        _rb.position = _initialPosition + Vector3.up * _snackItemAmplitude * Mathf.Sin(timeDiff / _snackItemPeriod * 2 * Mathf.PI);
    }
}
