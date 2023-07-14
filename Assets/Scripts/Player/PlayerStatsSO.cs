using UnityEngine;

[CreateAssetMenu]
public class PlayerStatsSO: ScriptableObject
{
    [Header("Layers")]
    public LayerMask PlayerLayer;
    public LayerMask WallLayer;
    public LayerMask ClimbableLayer;
    public LayerMask LadderLayer;

    [Header("Input")]
    public float VerticalDeadzoneThreshold = 0.3f;
    public float HorizontalDeadzoneThreshold = 0.1f;

    [Header("Movement")]
    public float MaxSpeed = 14;
    public float Acceleration = 120f;
    public float GroundDeceleration = 60f;

    public float AirDeceleration = 30f;
    public float GroundingForce = -1.5f;

    public float GrounderDistance = 0.05f;

    [Header("Jump")]
    public float JumpPower = 30f;
    public float MaxFallSpeed = 40f;
    public float FallAcceleration = 100f;
    public float JumpCutModifier = 0.4f;
    public float JumpEndEarlyGravityModifier = 3f;

    public float CoyoteTime = 0.3f;
    public float JumpBufferTime = 0.3f;

    [Header("Walls")]
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
    public int ExternalVelocityDecay = 100;
}
