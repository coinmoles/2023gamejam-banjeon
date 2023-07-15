public class NpcWorkAggroSlider : AggroSlider<NpcWork> 
{
    private NpcSnack _npcSnack;

    protected override void Awake()
    {
        base.Awake();
        _npcSnack = GetComponentInParent<NpcSnack>();
    }

    protected override void Update()
    {
        if (_npcSnack.IsAggro)
            return;
        base.Update();
    }
}
