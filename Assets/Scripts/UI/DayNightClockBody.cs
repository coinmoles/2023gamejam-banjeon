using ScriptableObjectVariable;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayNightClockBody : MonoBehaviour
{
    [SerializeField] private Sprite _spriteDusk;
    [SerializeField] private Sprite _spriteNoon;
    [SerializeField] private Sprite _spriteDawn;
    [SerializeField] private Sprite _spriteNight;

    [Header("Day Night Cycle")]
    [SerializeField] private BoolReference _isDay;
    [SerializeField] private FloatReference _dayNightStart;
    [SerializeField] private FloatReference _dayNightLength;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    // Update is called once per frame
    private void Update()
    {
        float timeFromDayNightStart = Time.time - _dayNightStart;
        if (_isDay)
        {
            if (timeFromDayNightStart <= _dayNightLength * 3 / 4)
                _image.sprite = _spriteNoon;
            else
                _image.sprite = _spriteDawn;
        }
        else
        {
            if (timeFromDayNightStart <= _dayNightLength * 3 / 4)
                _image.sprite = _spriteNight;
            else
                _image.sprite = _spriteDusk;
        }
    }
}
