using UnityEngine;

[CreateAssetMenu]
public class PlayerStatsSO: ScriptableObject
{
    [Header("Layers")]
    [Tooltip("Set this to the layer your player is on")]
    public LayerMask PlayerLayer;
    [Tooltip("Set this to the blocking layer that you will use (floor, ceil, wall etc)")]
    public LayerMask GroundLayer;
    public LayerMask CeilLayer;
    [Tooltip("Set this to the climablewall Layer")]
    public LayerMask ClimbableLayer;
    [Tooltip("Set this to the ladder layer")]
    public LayerMask LadderLayer;

    [Header("Input")]
    [Tooltip("Minimum input req'd before you mount a ladder. Avoids unwanted climbing"), Range(0.01f, 0.99f)]
    public float VerticalDeadzoneThreshold = 0.3f;
    [Tooltip("Minimum input req'd before a left or right is recognized. Avoids drifting"), Range(0.01f, 0.99f)]
    public float HorizontalDeadzoneThreshold = 0.1f;

    [Header("Movement")]
    public float MaxSpeed = 14;
    public float Acceleration = 120f;

    [Tooltip("Deceleration in ground/air only after stopping input mid-air")]
    public float GroundDeceleration = 60f;
    public float AirDeceleration = 30f;

    [Tooltip("Constant Downward force that is applied when grounded")]
    public float GroundingForce = -1.5f;
    public float GrounderDistance = 0.05f;

    [Header("Ladders")]
    public bool AllowLadders = true;
    public bool AutoAttachToLadders = true;
    public bool SnapToLadders = true;
    public float LadderSnapTime = 0.05f;
    public float LadderShimmySpeedMultiplier = 0.5f;
    public float LadderClimbSpeed = 6f;
    public float LadderSlideSpeed = 12f;
    public float LadderCooldownTime = 0.5f;
    public float LadderPopForce = 20f;

    [Header("Jump")]
    public float JumpPower = 30f;
    public float MaxFallSpeed = 40f;
    public float FallAcceleration = 100f;
    public float JumpCutModifier = 0.4f;
    public float JumpEndEarlyGravityModifier = 3f;

    public float CoyoteTime = 0.3f;
    public float JumpBufferTime = 0.3f;

    [Header("Walls")]
    [Tooltip("Toggle if Allow wall stick run, and climb")]
    public bool AllowWalls = true;
    public bool RequireInputPush = false;
    public float WallClimbSpeed = 5;
    public float WallFallAcceleration = 8;
    public float MaxWallFallSpeed = 15;
    public Vector2 WallJumpPower = new(30, 25);
    public float WallJumpInputLossTime = 0.6f;
    public float WallJumpCoyoteTime = 0.3f;
    public Vector2 WallDetectorSize = new(0.75f, 1.25f);

    [Header("External")]
    [Tooltip("For external Forces")]
    public int ExternalVelocityDecay = 100;
}
