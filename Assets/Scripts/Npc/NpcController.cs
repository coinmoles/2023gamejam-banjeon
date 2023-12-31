using UnityEngine;

public class NpcController : ObjectController
{
    private NpcAI _npcAI;

    protected override void Awake()
    {
        base.Awake();
        _npcAI = GetComponent<NpcAI>();
    }

    protected virtual void Update()
    {
        GatherInput();
    }

    protected void FixedUpdate()
    {
        // Debug.Log(_input.FrameInput.Move);
        _fixedTime += Time.fixedDeltaTime;

        CheckCollisions();
        HandleCollisions();
        HandleLadders();
        HandleWalls();

        HandleJump();
        ApplyMovement();
        HandleJump();

        HandleActions();

        HandleHorizontal();
        HandleVertical();
        ApplyMovement();
    }

    protected virtual void GatherInput()
    {
        FrameInput = _npcAI.GetFrameInput();
    }
}