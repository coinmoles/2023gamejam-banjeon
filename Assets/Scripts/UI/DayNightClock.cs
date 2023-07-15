using ScriptableObjectVariable;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayNightClock : MonoBehaviour
{
    [SerializeField] private List<Sprite> _sprites;
    [SerializeField] private int _currentSpriteIndex = 0;

    [Header("Day Night Cycle")]
    [SerializeField] private BoolReference _isDay;
    [SerializeField] private FloatReference _dayNightStart;
    [SerializeField] private FloatReference _dayNightLength;

    private Image _image;

    public float UnitTime { get { return _dayNightLength / (float)_sprites.Count; } }

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    // Update is called once per frame
    private void Update()
    {
        float timeFromDayNightStart = Time.time - _dayNightStart;
        if (timeFromDayNightStart > (_currentSpriteIndex + 1) * UnitTime)
        {
            _currentSpriteIndex = (_currentSpriteIndex + 1) % _sprites.Count;
            _image.sprite = _sprites[_currentSpriteIndex];
        }
    }
}
