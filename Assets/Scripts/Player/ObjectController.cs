using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ObjectController : MonoBehaviour, IPlayerController
{
    [SerializeField] protected PlayerStatsSO _stats;

    #region Internal

    protected Rigidbody2D _rb;
    [SerializeField] protected CapsuleCollider2D _col;

    protected FrameInput FrameInput;
    protected Vector2 _speed;

    protected float _fixedTime;
    protected bool _hasControl = true;
    #endregion

    #region External
    public event Action<bool, float> GroundedChanged;
    public event Action<bool> WallGrabChanged;
    public event Action<bool> Jumped;

    public PlayerStatsSO Stats => _stats;
    public Vector2 Input => FrameInput.Move;
    public Vector2 Velocity => _rb.velocity;
    public Vector2 Speed => _speed;
    public Vector2 GroundNormal { get; private set; }
    public int WallDirection { get; private set; }
    public bool Hiding { get; private set; }
    #endregion

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    
    #region Collision

    private RaycastHit2D[] _groundHits = new RaycastHit2D[2];
    private RaycastHit2D[] _ceilingHits = new RaycastHit2D[2];
    private readonly Collider2D[] _wallHits = new Collider2D[2];
    private RaycastHit2D _hittingWall;

    private int _groundHitCount;
    private int _ceilingHitCount;
    private int _wallHitCount;

    private float _timeLeftGrounded = float.MinValue;
    private bool _grounded;
    private Vector2 _skinWidth = new(0.02f, 0.02f);

    protected virtual void CheckCollisions()
    {
        _groundHitCount = Physics2D.CapsuleCastNonAlloc(_col.bounds.center, _col.size, _col.direction, 0, Vector2.down, _groundHits, _stats.GrounderDistance, _stats.WallLayer);
        _ceilingHitCount = Physics2D.CapsuleCastNonAlloc(_col.bounds.center, _col.size, _col.direction, 0, Vector2.up, _ceilingHits, _stats.GrounderDistance, _stats.WallLayer);

        // Walls and Ladders
        var bounds = GetWallDetectionBounds();
        _wallHitCount = Physics2D.OverlapBoxNonAlloc(bounds.center, bounds.size, 0, _wallHits, _stats.ClimbableLayer);

        _hittingWall = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, new Vector2(FrameInput.Move.x, 0), _stats.GrounderDistance, ~_stats.PlayerLayer);
    }

    protected virtual Bounds GetWallDetectionBounds()
    {
        var colliderOrigin = _rb.position + _col.offset;
        return new Bounds(colliderOrigin, _stats.WallDetectorSize);
    }

    protected virtual bool TryGetGroundNormal(out Vector2 groundNormal)
    {
        var hit = Physics2D.Raycast(_rb.position, Vector2.down, _stats.GrounderDistance * 2, ~_stats.PlayerLayer);
        groundNormal = hit.normal; // defaults to Vector2.zero if nothing was hit
        return hit.collider;
    }

    protected virtual void HandleCollisions()
    {
        // Hit a Ceiling
        if (_ceilingHitCount > 0)
        {
            _speed.y = Mathf.Min(0, _speed.y);
        }

        // Landed on the Ground
        if (!_grounded && _groundHitCount > 0)
        {
            _grounded = true;
            ResetJump();
            GroundedChanged?.Invoke(true, Mathf.Abs(_speed.y));
        }

        // Left the Ground
        else if (_grounded && _groundHitCount == 0)
        {
            _grounded = false;
            _timeLeftGrounded = _fixedTime;
            GroundedChanged?.Invoke(false, 0);
        }
    }

    #endregion

    #region Walls

    private readonly ContactPoint2D[] _wallContacts = new ContactPoint2D[2];
    private float _currentWallJumpMoveMultiplier = 1f; // aka "Horizontal input influence"
    private int _lastWallDirection; 
    private float _timeLeftWall; 
    private bool _isLeavingWall;
    private bool _isOnWall;

    protected virtual void HandleWalls()
    {
        if (!_stats.AllowWalls) return;

        _currentWallJumpMoveMultiplier = Mathf.MoveTowards(_currentWallJumpMoveMultiplier, 1f, 1f / _stats.WallJumpInputLossTime * Time.deltaTime);

        if (_wallHitCount > 0 && _wallHits[0].GetContacts(_wallContacts) > 0)
        {
            WallDirection = (int)Mathf.Sign(_wallContacts[0].point.x - transform.position.x);
            _lastWallDirection = WallDirection;
        }
        else WallDirection = 0;

        if (!_isOnWall && ShouldStickToWall() && _speed.y <= 0) ToggleOnWall(true);
        else if (_isOnWall && !ShouldStickToWall()) ToggleOnWall(false);

        bool ShouldStickToWall()
        {
            if (WallDirection == 0 || _grounded) return false;
            return !_stats.RequireInputPush || (HorizontalInputPressed && Mathf.Sign(FrameInput.Move.x) == WallDirection);
        }
    }

    private void ToggleOnWall(bool on)
    {
        _isOnWall = on;
        if (on)
        {
            _speed = Vector2.zero;
            _bufferedJumpUsable = true;
            _wallJumpCoyoteUsable = true;
        }
        else
        {
            _timeLeftWall = _fixedTime;
            _isLeavingWall = false; // after we've left the wall
        }

        WallGrabChanged?.Invoke(on);
    }
    #endregion

    #region Jump

    protected bool _jumpToConsume;
    protected float _timeJumpWasPressed;
    protected bool _bufferedJumpUsable;
    protected bool _endedJumpEarly;
    protected bool _coyoteUsable;
    protected bool _wallJumpCoyoteUsable;

    protected virtual bool HasBufferedJump => _bufferedJumpUsable && _fixedTime < _timeJumpWasPressed + _stats.JumpBufferTime;
    protected virtual bool CanUseCoyote => _coyoteUsable && !_grounded && _fixedTime < _timeLeftGrounded + _stats.CoyoteTime;
    protected virtual bool CanWallJump => (_isOnWall && !_isLeavingWall) || (_wallJumpCoyoteUsable && _fixedTime < _timeLeftWall + _stats.WallJumpCoyoteTime);

    protected virtual void HandleJump()
    {
        if (!_endedJumpEarly && !_grounded && !FrameInput.JumpHeld && _rb.velocity.y > 0) _endedJumpEarly = true; // Early end detection
        if (!_jumpToConsume && !HasBufferedJump) return;

        if (CanWallJump) WallJump();
        else if (_grounded || CanUseCoyote) NormalJump();

        _jumpToConsume = false; // Always consume the flag
    }

    private void NormalJump()
    {
        _endedJumpEarly = false;
        _timeJumpWasPressed = 0;
        _bufferedJumpUsable = false;
        _coyoteUsable = false;
        _speed.y = _stats.JumpPower;
        Jumped?.Invoke(false);
    }

    private void WallJump()
    {
        _endedJumpEarly = false;
        _bufferedJumpUsable = false;
        if (_isOnWall) _isLeavingWall = true;
        _wallJumpCoyoteUsable = false;
        _currentWallJumpMoveMultiplier = 0;
        _speed = Vector2.Scale(_stats.WallJumpPower, new(-_lastWallDirection, 1));
        Jumped?.Invoke(true);
    }

    protected virtual void ResetJump()
    {
        _coyoteUsable = true;
        _bufferedJumpUsable = true;
        _endedJumpEarly = false;
    }
    #endregion

    #region Horizontal

    protected virtual bool HorizontalInputPressed => Mathf.Abs(FrameInput.Move.x) > _stats.HorizontalDeadzoneThreshold;

    protected virtual void HandleHorizontal()
    {
        if(!HorizontalInputPressed)
        {
            var deceleataion = _grounded ? _stats.GroundDeceleration : _stats.AirDeceleration;
            _speed.x = Mathf.MoveTowards(_speed.x, 0, deceleataion * Time.fixedDeltaTime);
        }
        else
        {
            if (_hittingWall.collider && Mathf.Abs(_rb.velocity.x) < 0.01f && !_isLeavingWall) _speed.x = 0;

            var xInput = FrameInput.Move.x;
            _speed.x = Mathf.MoveTowards(_speed.x, xInput * _stats.MaxSpeed, _currentWallJumpMoveMultiplier * _stats.Acceleration * Time.fixedDeltaTime);
        }
    }
    #endregion

    #region Vertical
    protected virtual void HandleVertical()
    {
        // Grounded & Slopes
        if (_grounded && _speed.y <= 0f)
        {
            _speed.y = _stats.GroundingForce;

            if (TryGetGroundNormal(out var groundNormal))
            {
                GroundNormal = groundNormal;
                if (!Mathf.Approximately(GroundNormal.y, 1f))
                {
                    // on a slope
                    _speed.y = _speed.x * -GroundNormal.x / GroundNormal.y;
                    if (_speed.x != 0) _speed.y += _stats.GroundingForce;
                }
            }
        }
        // Wall Climbing & Sliding
        else if (_isOnWall && !_isLeavingWall)
        {
            if (FrameInput.Move.y > 0) _speed.y = _stats.WallClimbSpeed;
            else if (FrameInput.Move.y < 0) _speed.y = -_stats.MaxWallFallSpeed;
            else _speed.y = Mathf.MoveTowards(Mathf.Min(_speed.y, 0), -_stats.MaxWallFallSpeed, _stats.WallFallAcceleration * Time.fixedDeltaTime);
        }
        // In Air
        else
        {
            var inAirGravity = _stats.FallAcceleration;
            if (_endedJumpEarly && _speed.y > 0) inAirGravity *= _stats.JumpEndEarlyGravityModifier;
            _speed.y = Mathf.MoveTowards(_speed.y, -_stats.MaxFallSpeed, inAirGravity * Time.fixedDeltaTime);
        }
    }
    #endregion

    protected virtual void ApplyMovement()
    {
        Debug.Log(_speed);
        _rb.velocity = _speed;
    }

}

public interface IPlayerController
{
    // true = Landed. false = Left the Ground. float is Impact Speed
    public event Action<bool, float> GroundedChanged;
    public event Action<bool> WallGrabChanged;
    public event Action<bool> Jumped; // Is wall jump


    public PlayerStatsSO Stats { get; }
    public Vector2 Input { get; }
    public Vector2 Speed { get; }
    public Vector2 Velocity { get; }
    public Vector2 GroundNormal { get; }
    public int WallDirection { get; }
}