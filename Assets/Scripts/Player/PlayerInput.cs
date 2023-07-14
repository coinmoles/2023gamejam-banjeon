using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public FrameInput FrameInput { get; private set; }

    private void Update()
    {
        FrameInput = Gather();    
    }

    private FrameInput Gather()
    {
        return new FrameInput
        {
            JumpDown = Input.GetKeyDown(KeyCode.Z),
            JumpHeld = Input.GetKey(KeyCode.Z),
            JumpUp = Input.GetKeyUp(KeyCode.Z),

            Move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")),

            ChangeDown = Input.GetKeyDown(KeyCode.Z)
        };
    }
}

public struct FrameInput
{
    public Vector2 Move;
    public bool JumpDown;
    public bool JumpHeld;
    public bool JumpUp;

    public bool ChangeDown;
}