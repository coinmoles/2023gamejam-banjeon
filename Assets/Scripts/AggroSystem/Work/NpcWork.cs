using UnityEngine;

public class NpcWork : MonoBehaviour, IAggroSlidable
{
    [Header("Aggro Data")]
    [SerializeField] private WorkSO _linkedWork;

    [Header("Aggro State")]
    [SerializeField] private bool _isWorking;
    [SerializeField] private bool _isGoingHome;
    [SerializeField] private Vector2 _homeLocation;

    private NpcAI _npcAI;


    public bool IsAggro => _isWorking;
    public float MaxAggroTime => _linkedWork.MaxAggroTime;
    public float AggroEndTime => _linkedWork.EndTime;
    public Sprite AggroSliderImage => _linkedWork.WorkImage;

    public bool IsGoingHome => _isGoingHome;
    
    public Vector2 WorkPosition => _linkedWork.WorkPosition;
    public Vector2 HomePosition => _homeLocation;

    #region Lifecycle Functions

    private void Awake()
    {
        _npcAI = GetComponent<NpcAI>();
    }

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

    #region Work Functions
    public void OnWorkSabotaged()
    {
        _isWorking = true;
        _isGoingHome = false;
        _npcAI.GoState = GoState.JustStarted;
    }

    public void OnWorkEnded()
    {
        _isWorking = false;
        _isGoingHome = true;
        _npcAI.GoState = GoState.JustStarted;
    }

    public void HomeArrived()
    {
        _isGoingHome = false;
        _npcAI.GoState = GoState.JustStarted;
    }
    #endregion
}