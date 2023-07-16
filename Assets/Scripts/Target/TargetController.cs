using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : ObjectController
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected virtual void Update()
    {
        GatherInput();
    }

    protected void FixedUpdate()
    {
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
        FrameInput = new FrameInput();
    }
}
