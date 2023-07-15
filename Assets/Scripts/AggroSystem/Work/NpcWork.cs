using ScriptableObjectVariable;
using System.Collections;
using UnityEngine;

public class NpcWork : MonoBehaviour
{
    [Header("Aggro Data")]
    [SerializeField] public WorkSO LinkedWork;

    [Header("Aggro State")]
    [SerializeField] private bool _isWorking;
    [SerializeField] private bool _isGoingHome;
    [SerializeField] private Vector2 _homeLocation;

    public bool IsCheckingWork => _isWorking;
    public bool IsGoingHome => _isGoingHome;
    public Vector2 WorkPosition => LinkedWork.WorkPosition;
    public Vector2 HomePosition => _homeLocation;

    #region Lifecycle Functions
    private void Start()
    {
        _homeLocation = transform.position;
    }

    private void OnEnable()
    {
        LinkedWork.RegisterListener(this);
    }

    private void OnDisable()
    {
        LinkedWork.UnregisterListener(this);
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