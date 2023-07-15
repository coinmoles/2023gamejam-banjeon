using UnityEngine;
using UnityEngine.UI;
using ScriptableObjectVariable;

public abstract class AggroSlider : MonoBehaviour
{
    private Slider _slider;

    protected abstract bool IsAggro { get; }
    protected abstract float MaxAggroTime { get; }
    protected abstract float AggroEndTime { get; }

    protected virtual void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Update()
    {
        if (IsAggro)
            _slider.value = (AggroEndTime - Time.time) / MaxAggroTime;
    }
}