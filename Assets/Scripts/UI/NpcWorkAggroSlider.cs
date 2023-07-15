using UnityEngine;
using UnityEngine.UI;

public class NpcWorkAggroSlider : AggroSlider
{
    [SerializeField] private WorkSO _linkedWork;

    protected override bool IsAggro { get { return _linkedWork.EndTime > Time.time; } }
    protected override float MaxAggroTime { get { return _linkedWork.MaxAggroTime; } }
    protected override float AggroEndTime { get { return _linkedWork.EndTime; } }

    protected override void Awake()
    {
        base.Awake();
        _linkedWork = GetComponentInParent<NpcWork>().LinkedWork;
    }
}
