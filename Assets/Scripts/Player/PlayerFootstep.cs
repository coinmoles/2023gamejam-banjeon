using ScriptableObjectVariable;
using UnityEngine;

public class PlayerFootstep: MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip _redWalk;
    [SerializeField] private AudioClip _wolfWalk;

    [SerializeField] private BoolReference _isDay;

    private PlayerInput _playerInput;
    private PlayerController _playerController;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (_playerController.Grounded && _playerInput.FrameInput.Move.x != 0)
        {
            _audioSource.clip = _isDay ? _redWalk : _wolfWalk;
            if (!_audioSource.isPlaying)
                _audioSource.Play();
        }
        else if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
    }
}
