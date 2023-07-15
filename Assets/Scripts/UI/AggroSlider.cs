using UnityEngine;
using UnityEngine.UI;

public interface IAggroSlidable
{
    public bool IsAggro { get; }
    public float MaxAggroTime { get; }
    public float AggroEndTime { get; }
    public Sprite AggroSliderImage { get; }
}

public abstract class AggroSlider<T>: MonoBehaviour where T: IAggroSlidable
{
    private Slider _slider;
    private Image _image;
    private T _aggroSlidable;

    protected virtual void Awake()
    {
        _slider = GetComponent<Slider>();
        _aggroSlidable = GetComponentInParent<T>();
        _image = GetComponentInChildren<Image>();
    }

    protected virtual void Start()
    {
        _image.sprite = _aggroSlidable.AggroSliderImage;
    }

    protected virtual void Update()
    {
        if (_aggroSlidable.IsAggro)
            _slider.value = (_aggroSlidable.AggroEndTime - Time.time) / _aggroSlidable.MaxAggroTime;
        else if (_slider.value != 0)
            _slider.value = 0;
    }
}