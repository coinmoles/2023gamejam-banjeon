using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : ObjectController
{
    [SerializeField] private PlayerStatsSO RedStats;
    [SerializeField] private PlayerStatsSO WolfStats;
    protected PlayerInput _input;

    protected override void Awake()
    {
        _input = GetComponent<PlayerInput>();
        ToggleChanged(isDay: true);
        base.Awake();
    }

    private void Start()
    {
        ToggleChanged(isDay: true);
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
        FrameInput = _input.FrameInput;

        if (FrameInput.JumpDown)
        {
            _jumpToConsume = true;
            _timeJumpWasPressed = _fixedTime;
        }
        if(FrameInput.ActionDown)
        {
            _actionToConsume = true;
        }
    }

    #region DayNight

    protected override void ToggleChanged(bool isDay)
    {
        base.ToggleChanged(isDay);
        _stats = isDay ? RedStats : WolfStats;
    }
    #endregion

    #region Actions

    protected override void HandleActions()
    {
        base.HandleActions();
    }

    #endregion
}
