using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimator : MonoBehaviour
{
    private IPlayerController _player;
    private Animator _anim;
    private SpriteRenderer _renderer;
    private AudioSource _source;

    [SerializeField] private Transform _weaponHolder;
    
    private void Awake()
    {
        _player = transform.parent.GetComponentInParent<IPlayerController>();

        _player.OnAction += OnAction;
        _player.DayChanged += OnDayChanged;

        _anim = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        // _source = GetComponent<AudioSource>();

        _attacked = false;
    }

    private float _time;
    private void Update()
    {
        // Debug.Log(_attacked);
        _time += Time.deltaTime;
        HandleSpriteFlipping();
        HandleAnimations();
    }

    private void HandleSpriteFlipping()
    {
        if (_isOnWall && _player.WallDirection != 0) _weaponHolder.localScale = new Vector3(-_player.WallDirection, 1f, 1f);
        else if (Mathf.Abs(_player.Input.x) > 0.1f) _weaponHolder.localScale = new Vector3(_player.Input.x > 0 ? 1 : -1, 1f, 1f);
    }

    #region Day Night

    private bool _isDay;
    private void OnDayChanged(bool isDay)
    {
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
            // Debug.Log(_attacked);
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
