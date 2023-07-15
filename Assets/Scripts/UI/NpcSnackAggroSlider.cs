using ScriptableObjectVariable;
using UnityEngine;
using UnityEngine.UI;

public class NpcSnackAggroSlider : AggroSlider
{
    private NpcSnack _npcSnack;

    protected override bool IsAggro { get { return _npcSnack.IsEating; } }
    protected override float MaxAggroTime { get { return _npcSnack.SnackAggroTime; } }
    protected override float AggroEndTime { get { return _npcSnack.AggroEndTime; } }

    protected override void Awake()
    {
        base.Awake();
        _npcSnack = GetComponentInParent<NpcSnack>();
    }
}
