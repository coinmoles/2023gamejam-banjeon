using ScriptableObjectVariable;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private FloatReference _playerMaxHealth;
    [SerializeField] private FloatReference _playerCurrentHealth;

    [SerializeField] private GameEvent _onGameOver;

    public void OnPlayerHit()
    {
        _playerCurrentHealth.SetValue(_playerCurrentHealth - 1);


        if (_playerCurrentHealth == 0)
            _onGameOver.Raise(this, null);
    }
}
