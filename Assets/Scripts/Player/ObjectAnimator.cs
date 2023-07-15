using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAnimator : MonoBehaviour
{
    private IPlayerController _player;
    private Animator _anim;
    private SpriteRenderer _renderer;
    private AudioSource _source;

    private void Awake()
    {
        _player = GetComponentInParent<IPlayerController>();

        _anim = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        // _source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        HandleSpriteFlipping();
        HandleAnimations();
    }

    private void HandleSpriteFlipping()
    {
        if (Mathf.Abs(_player.Input.x) > 0.1f) _renderer.flipX = _player.Input.x < 0;
    }

    #region Animation

    private float _lockedTill;

    private void HandleAnimations()
    {
        var state = GetState();
        if (state == _currentState) return;

        _anim.Play(state, 0); //_anim.CrossFade(state, 0, 0);
        _currentState = state;

        int GetState()
        {
            if (Time.time < _lockedTill) return _currentState;

            return _player.Input.x == 0 ? Idle : Run;
            
        }

    }

    private void UnlockAnimationLock() => _lockedTill = 0f;

    #endregion


    #region Cached Properties

    private int _currentState;

    private static readonly int Run = Animator.StringToHash("Run");
    private static readonly int Idle = Animator.StringToHash("Idle");
    #endregion
}
