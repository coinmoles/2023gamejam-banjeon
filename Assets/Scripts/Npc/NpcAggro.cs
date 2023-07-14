using ScriptableObjectVariable;
using System.Collections;
using UnityEngine;

public class NpcAggro : MonoBehaviour
{
    [Header("Aggro Data")]
    [SerializeField] private Snack _likedSnack;
    [SerializeField] private NpcWork _linkedWork;
    [SerializeField] private FloatReference _aggroTime;

    [Header("Aggro State")]
    [SerializeField] private bool _isEating;
    [SerializeField] private bool _isCheckingWork;
    [SerializeField] private bool _isGoingHome;
    [SerializeField] private Vector2 _homeLocation;
    [SerializeField] private Vector2 _workLocation;

    public bool IsEating => _isEating;
    public bool IsCheckingWork => _isCheckingWork;
    public bool IsGoingHome => _isGoingHome;
    public Vector2 WorkLocation => _workLocation;
    public Vector2 HomeLocation => _homeLocation;

    private void Start()
    {
        _homeLocation = transform.position;
    }

    private bool IsLikedSnack(Snack snack)
    {
        return snack == _likedSnack;
    }

    public void OnWorkCreated(Component sender, object data)
    {
        Debug.Assert(data is WorkAndLoc, "Not correct work data for event workcreated");
        WorkAndLoc createdWork = (WorkAndLoc)data;

        if (createdWork.work == _linkedWork && !_isEating && !_isCheckingWork)
        {
            _isCheckingWork = true;
            StartCoroutine(EndCheckingWork());
        }
    }

    public void GetSnack(Snack snack)
    {
        if (snack == _likedSnack && !_isEating && !_isCheckingWork)
        {
            _isEating = true;
            StartCoroutine(EndEating());
        }
    }

    public void HomeArrived()
    {
        _isGoingHome = false;
    }

    private IEnumerator EndCheckingWork ()
    {
        yield return new WaitForSeconds(_aggroTime);
        _isCheckingWork = false;
        _isGoingHome = true;
    }
    private IEnumerator EndEating()
    {
        yield return new WaitForSeconds(_aggroTime);
        _isCheckingWork = false;
    }
}
