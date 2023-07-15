using ScriptableObjectVariable;
using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour
{
    [Header("Constants")]
    [SerializeField] private FloatReference _dayNightLength;

    [Header("World States")]
    [SerializeField] private FloatReference _dayNightStart;
    [SerializeField] private BoolReference _isDay;

    [Header("Game Events")]
    [SerializeField] private GameEvent _onDayStart;
    [SerializeField] private GameEvent _onNightStart;

    private void Start()
    {
        StartCoroutine(ChangeDayNight());
    }

    private IEnumerator ChangeDayNight()
    { 
        while(true)
        {
            yield return new WaitForSeconds(_dayNightLength);
            if (_isDay)
                _onNightStart.Raise(this, null);
            else
                _onDayStart.Raise(this, null);
        }
    }
    
    public void OnDayStart(Component sender, object data)
    {
        _isDay.SetValue(true);
        _dayNightStart.SetValue(Time.time);
    }

    public void OnNightStart(Component sender, object data)
    {
        _isDay.SetValue(false);
        _dayNightStart.SetValue(Time.time);
    }
}
