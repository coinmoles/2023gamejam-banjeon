using ScriptableObjectVariable;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

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
        _onDayStart.Raise(this, null);
        lastIsDay = true;
        _coroutine = StartCoroutine(ChangeDayNight());
    }

    private bool lastIsDay;
    private IEnumerator ChangeDayNight()
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
        if (_coroutine != null)
            StopCoroutine(_coroutine);
        lastIsDay = true;
        _coroutine = StartCoroutine(ChangeDayNight());
    }

    public void OnNightStart(Component sender, object data)
    {
        _isDay.SetValue(false);
        _dayNightStart.SetValue(Time.time);
        if (_coroutine != null)
            StopCoroutine(_coroutine);
        lastIsDay = false;
        _coroutine = StartCoroutine(ChangeDayNight());
    }
}
