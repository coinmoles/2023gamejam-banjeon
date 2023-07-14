using ScriptableObjectVariable;
using System.Collections;
using UnityEngine;

public class NpcAggro : MonoBehaviour
{
    [Header("Aggro Data")]
    [SerializeField] private SnackSO _likedSnack;
    [SerializeField] private WorkSO _linkedWork;
    [SerializeField] private FloatReference _aggroTime;

    [Header("Aggro State")]
    [SerializeField] private bool _isEating;
    [SerializeField] private bool _isWorking;
    [SerializeField] private bool _isGoingHome;
    [SerializeField] private Vector2 _homeLocation;

    public bool IsEating => _isEating;
    public bool IsCheckingWork => _isWorking;
    public bool IsGoingHome => _isGoingHome;
    public Vector2 WorkPosition => _linkedWork.WorkPosition;
    public Vector2 HomePosition => _homeLocation;

    #region Lifecycle Functions
    private void Start()
    {
        _homeLocation = transform.position;
    }

    private void OnEnable()
    {
        _linkedWork.RegisterListener(this);
    }

    private void OnDisable()
    {
        _linkedWork.UnregisterListener(this);
    }
    #endregion

    #region Snack Functions
    private bool IsLikedSnack(SnackSO snack)
    {
        return snack == _likedSnack;
    }

    public void ObtainSnack(SnackSO snack)
    {
        if (snack == _likedSnack && !_isEating && !_isWorking)
        {
            _isEating = true;
            StartCoroutine(EndEating());
        }
    }
    private IEnumerator EndEating()
    {
        yield return new WaitForSeconds(_aggroTime);
        _isEating = false;
    }
    #endregion

    #region Work Functions
    public void OnWorkSabotaged()
    {
        _isWorking = true;
        _isGoingHome = false;
    }
    
    public void OnWorkEnded()
    {
        _isWorking = false;
        _isGoingHome = true;
    }

    public void HomeArrived()
    {
        _isGoingHome = false;
    }
    #endregion
}