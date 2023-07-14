using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ObjectController
{
    protected PlayerInput _input;

    protected override void Awake()
    {
        _input = GetComponent<PlayerInput>();
        base.Awake();
    }

    protected virtual void Update()
    {
        GatherInput();
    }

    private void FixedUpdate()
    {
        // Debug.Log(_input.FrameInput.Move);
        _fixedTime += Time.fixedDeltaTime;

        CheckCollisions();
        HandleCollisions();
        HandleWalls();

        HandleJump();
        ApplyMovement();
        HandleJump();

        HandleHorizontal();
        HandleVertical();
        ApplyMovement();
    }

    protected virtual void GatherInput()
    {
        FrameInput = _input.FrameInput;

        if (FrameInput.JumpDown)
        {
            _jumpToConsume = true;
            _timeJumpWasPressed = _fixedTime;
        }
    }
}
