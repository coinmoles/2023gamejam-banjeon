using ScriptableObjectVariable;
using UnityEngine;
using UnityEngine.UI;

public class DayNightClockMask : MonoBehaviour
{
    [Header("Day Night Cycle")]
    [SerializeField] private BoolReference _isDay;
    [SerializeField] private FloatReference _dayNightStart;
    [SerializeField] private FloatReference _dayNightLength;

    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        float timeFromDayNightStart = Time.time - _dayNightStart;
        _slider.value = timeFromDayNightStart / _dayNightLength;
    }
}
