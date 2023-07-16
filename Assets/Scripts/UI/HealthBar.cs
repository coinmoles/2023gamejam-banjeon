using ScriptableObjectVariable;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private FloatReference _playerMaxHealth;
    [SerializeField] private FloatReference _playerCurrentHealth;

    [SerializeField] private Image[] hearts = new Image[3];

    public void OnPlayerHit()
    {
        for (int i = 0; i < _playerMaxHealth; i++)
        {
            hearts[i].enabled = i < _playerCurrentHealth;
        }
    }
}
