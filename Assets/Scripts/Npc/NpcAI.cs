using UnityEngine;

public abstract class NpcAI : MonoBehaviour
{
    private NpcWork _npcWork;
    private NpcSnack _npcSnack;

    protected readonly FrameInput NO_INPUT = new FrameInput()
    {
        Move = new Vector2(0, 0)
    };


    private void Awake()
    {
        _npcWork = GetComponent<NpcWork>();
        _npcSnack = GetComponent<NpcSnack>();
    }

    public FrameInput GetFrameInput()
    {
        Debug.Log("Buster");
        if (_npcSnack.IsEating)
            return NO_INPUT;
        else if (_npcWork.IsCheckingWork)
            return new FrameInput()
            {
                Move = new Vector2(CalcDirToLocation(_npcWork.WorkPosition), 0)
            };
        else if (_npcWork.IsGoingHome)
        {
            Debug.Log("OK?");
            if (Mathf.Abs(_npcWork.HomePosition.x - transform.position.x) < 0.2f)
            {
                _npcWork.HomeArrived();
            }
            return new FrameInput()
            {
                Move = new Vector2(CalcDirToLocation(_npcWork.HomePosition), 0)
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
