using UnityEngine;

public abstract class NpcAI : MonoBehaviour
{
    private NpcAggro _npcAggro;

    protected readonly FrameInput NO_INPUT = new FrameInput()
    {
        Move = new Vector2(0, 0)
    };


    private void Awake()
    {
        _npcAggro = GetComponent<NpcAggro>();
    }

    public FrameInput GetFrameInput()
    {
        if (_npcAggro.IsEating)
            return NO_INPUT;
        else if (_npcAggro.IsCheckingWork)
            return new FrameInput()
            {
                Move = new Vector2(CalcDirToLocation(_npcAggro.WorkPosition), 0)
            };
        else if (_npcAggro.IsGoingHome)
        {
            if (Mathf.Abs(_npcAggro.HomePosition.x - transform.position.x) < 0.2f)
            {
                _npcAggro.HomeArrived();
            }
            return new FrameInput()
            {
                Move = new Vector2(CalcDirToLocation(_npcAggro.HomePosition), 0)
            };
        }

        return CalcDefaultInput();
    }

    private float CalcDirToLocation(Vector2 location)
    {
        float distanceToLocation = location.x - transform.position.x;
        float dirToLocation = Mathf.Sign(distanceToLocation);
        if (Mathf.Abs(distanceToLocation) <= 0.2f) dirToLocation = 0;

        return dirToLocation;
    }

    protected abstract FrameInput CalcDefaultInput();
}
