using UnityEngine;
using UnityEngine.UI;

public interface IAggroSlidable
{
    public bool IsAggro { get; }
    public float MaxAggroTime { get; }
    public float AggroEndTime { get; }
        
}

public abstract class AggroSlider<T>: MonoBehaviour where T: IAggroSlidable
{
    private Slider _slider;
    private T _aggroSlidable;


    protected virtual void Awake()
    {
        _slider = GetComponent<Slider>();
        _aggroSlidable = GetComponentInParent<T>();
    }

    protected virtual void Update()
    {
        if (_aggroSlidable.IsAggro)
            _slider.value = (_aggroSlidable.AggroEndTime - Time.time) /_aggroSlidable.MaxAggroTime;
    }
}