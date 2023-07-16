using ScriptableObjectVariable;
using UnityEngine;

public enum GoState
{
    JustStarted,
    OnSameLevel,
    OnLowerLevel,
    OnHigherLevel,
    OnHigherLevelLadder,
    OnLowerLevelLadder
}

public abstract class NpcAI : MonoBehaviour
{
    [SerializeField] private FloatReference _ladderX;

    private NpcWork _npcWork;
    private NpcSnack _npcSnack;

    public GoState GoState;

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
        if (_npcSnack.IsEating)
            return NO_INPUT;
        else if (_npcWork.IsAggro)
        {
            return CalcFrameInputToLocation(_npcWork.WorkPosition);
        }
        else if (_npcWork.IsGoingHome)
        {
            if (Mathf.Abs(_npcWork.HomePosition.x - transform.position.x) < 0.1f)
            {
                _npcWork.HomeArrived();
            }
            return CalcFrameInputToLocation(_npcWork.HomePosition);
        }

        return CalcDefaultInput();
    }

    private FrameInput CalcFrameInputToLocation(Vector2 location)
    {
        if (GoState == GoState.JustStarted)
        {
            // Check if it's on same, lower or higher level compared to location
            if (Mathf.Abs(location.y - transform.position.y) < 0.5f)
                GoState = GoState.OnSameLevel;
            else if (location.y - transform.position.y > 0)
                GoState = GoState.OnLowerLevel;
            else if (location.y - transform.position.y < 0)
                GoState = GoState.OnHigherLevel;
        }
        else if (GoState == GoState.OnSameLevel)
            // If on same level, just bline straight to it
            return new FrameInput()
            {
                Move = new Vector2(CalcDirToLocation(location), 0)
            };
        else if (GoState == GoState.OnLowerLevel || GoState == GoState.OnLowerLevelLadder)
        {
            // Go to ladder
            if (Mathf.Abs(_ladderX - transform.position.x) > 0.1f)
            {
                Vector2 ladderLocation = new Vector2(_ladderX, transform.position.y);
                return new FrameInput()
                {
                    Move = new Vector2(CalcDirToLocation(ladderLocation), 0)
                };
            }
            // Go up til you're above destination
            else if (transform.position.y - location.y < 0.1f)
            {
                GoState = GoState.OnLowerLevelLadder; 
                return new FrameInput()
                {
                    Move = Vector2.up
                };
            }
            else
                GoState = GoState.OnSameLevel;
        }
        else if (GoState == GoState.OnHigherLevel || GoState == GoState.OnHigherLevelLadder)
        {
            // Go to ladder
            if (Mathf.Abs(_ladderX - transform.position.x) > 0.1f)
            {
                Vector2 ladderLocation = new Vector2(_ladderX, transform.position.y);
                return new FrameInput()
                {
                    Move = new Vector2(CalcDirToLocation(ladderLocation), 0)
                };
            }
            else if (transform.position.y - location.y > 0.1f)
            {
                GoState = GoState.OnHigherLevelLadder;
                return new FrameInput()
                {
                    Move = Vector2.down
                };
            }
            else
                GoState = GoState.OnSameLevel;
        }

        return new FrameInput();
    }

    private float CalcDirToLocation(Vector2 location)
    {
        float distanceToLocation = location.x - transform.position.x;
        float dirToLocation = Mathf.Sign(distanceToLocation);
        if (Mathf.Abs(distanceToLocation) <= 0.1f) dirToLocation = 0;

        return dirToLocation;
    }

    protected abstract FrameInput CalcDefaultInput();
}
