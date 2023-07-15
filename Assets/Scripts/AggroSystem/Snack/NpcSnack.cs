using ScriptableObjectVariable;
using UnityEngine;

public class NpcSnack : MonoBehaviour, IAggroSlidable
{
    [Header("Aggro")]
    [SerializeField] private SnackSO _likedSnack;
    [SerializeField] private FloatReference _snackAggroTime;
    [SerializeField] private BoolReference _isEating;
    [SerializeField] private FloatReference _aggroEndTime;

    [Header("Day Night Cycle")]
    [SerializeField] private BoolReference _isDay;
    [SerializeField] private FloatReference _dayNightStart;
    [SerializeField] private FloatReference _dayNightLength;

    public bool IsAggro => _isEating;
    public float MaxAggroTime => _snackAggroTime;
    public float AggroEndTime => _aggroEndTime;


    public bool IsEating => _isEating;
    private void Update()
    {
        if (Time.time > _aggroEndTime)
        {
            _isEating.SetValue(false);
            enabled = false;
        }
    }

    #region Snack Functions
    public bool IsLikedSnack(SnackSO snack)
    {
        return snack == _likedSnack;
    }

    public bool GivenSnack(SnackSO snack)
    {
        if (snack == _likedSnack && !_isEating)
        {
            _isEating.SetValue(true);
            enabled = true;
            _aggroEndTime.SetValue(Time.time + _snackAggroTime);
            return true;
        }
        return false;
    }
    #endregion

    public void OnPlayerHit(Component sender, object data)
    {
        if (!_isDay)
        {
            float timeFromDayStart = Time.time - _dayNightStart;
            float timeTilDayEnd = _dayNightLength - timeFromDayStart;
            _aggroEndTime.SetValue(_aggroEndTime - timeTilDayEnd);
        }
    }
}