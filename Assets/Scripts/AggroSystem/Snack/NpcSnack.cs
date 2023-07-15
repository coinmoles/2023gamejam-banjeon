using ScriptableObjectVariable;
using System.Collections;
using UnityEngine;

public class NpcSnack : MonoBehaviour
{
    [Header("Aggro Data")]
    [SerializeField] private SnackSO _likedSnack;
    [SerializeField] private FloatReference _snackAggroTime;

    [Header("Aggro State")]
    [SerializeField] private bool _isEating;
    [SerializeField] private float _aggroEndTime;

    [Header("Day Night Cycle")]
    [SerializeField] private BoolReference _isDay;
    [SerializeField] private FloatReference _dayNightStart;
    [SerializeField] private FloatReference _dayNightLength;


    public bool IsEating => _isEating;
    private void Update()
    {
        if (Time.time > _aggroEndTime)
        {
            _isEating = false;
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
            _isEating = true;
            enabled = true;
            _aggroEndTime = Time.time + _snackAggroTime;
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
            _aggroEndTime -= timeTilDayEnd;
        }
    }
}