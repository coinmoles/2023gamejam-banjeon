using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimator : MonoBehaviour
{
    private IPlayerController _player;
    private Animator _anim;
    private SpriteRenderer _renderer;
    private AudioSource _source;

    private void Awake()
    {
        _player = GetComponentInParent<IPlayerController>();

        _player.OnAction += OnAction;
        _player.DayChanged += OnDayChanged;

        _anim = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        // _source = GetComponent<AudioSource>();
    }

    private float _time;
    private void Update()
    {
        _time += Time.deltaTime;
        HandleSpriteFlipping();
        HandleAnimations();
    }

    private void HandleSpriteFlipping()
    {
        if (_isOnWall & _player.WallDirection != 0) _renderer.flipX = _player.WallDirection == -1;
        else if (Mathf.Abs(_player.Input.x) > 0.1f) _renderer.flipX = _player.Input.x < 0;
    }

    #region Day Night

    private bool _isDay;
    private bool _isDayChanged = false;
    private float _changeAnimTime = 0.1f;
    private void OnDayChanged(bool isDay)
    {
        // Debug.Log("IsDay" + isDay.ToString());
        _isDayChanged = true;
        _isDay = isDay;
    }
    #endregion

    #region Wall
    private bool _hitWall, _isOnWall, _isSliding, _dismountedWall;

    private void OnWallGrabChanged(bool onWall)
    {
        _hitWall = _isOnWall = onWall;
        _dismountedWall = !onWall;
    }
    #endregion

    #region OnAction

    private void OnAction(bool isDay)
    {
        if(isDay)
        {

        }
        else
        {
            OnAttacked();
        }
    }
    #endregion

    #region OnAttacked
    private bool _attacked;
    [SerializeField] private float _attackAnimTime;

    private void OnAttacked()
    {
        _attacked = true;
    }
    #endregion

    #region Animaitons

    private float _lockedTill;

    private void HandleAnimations()
    {
        var state = GetState();
        ResetFlags();
        if (state == _currentState) return;

        _anim.Play(state, 0);
        _currentState = state;

        int GetState()
        {
            if (_time < _lockedTill) return _currentState;

            if (_attacked)
            {
                return LockState(Attack, _attackAnimTime);
            }

            int LockState(int s, float t)
            {
                _lockedTill = _time + t;
                return s;
            }

            return Idle;
        }

        void ResetFlags()
        {
            _attacked = false;
        }
    }

    private void UnlockAnimationLock() => _lockedTill = 0f;

    #endregion

    #region Cached Properties
    private int _currentState;

    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Attack = Animator.StringToHash("Attack");
    #endregion
}
