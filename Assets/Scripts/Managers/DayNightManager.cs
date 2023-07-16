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

    private Coroutine _coroutine;

    private void Start()
    {
        _coroutine = StartCoroutine(ChangeDayNight(true));
        _isDay.SetValue(true);
    }

    private IEnumerator ChangeDayNight(bool lastIsDay)
    {
        while(true)
        {
            yield return new WaitForSeconds(_dayNightLength);
            if (lastIsDay)
            {
                _onNightStart.Raise(this, null);
                lastIsDay = false;
            }
            else
            {
                _onDayStart.Raise(this, null);
                lastIsDay = true;
            }
        }
    }
    
    public void OnDayStart(Component sender, object data)
    {
        _isDay.SetValue(true);
        _dayNightStart.SetValue(Time.time);
        StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(ChangeDayNight(true));
    }

    public void OnNightStart(Component sender, object data)
    {
        _isDay.SetValue(false);
        _dayNightStart.SetValue(Time.time);
        StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(ChangeDayNight(false));
    }
}
