using UnityEngine;
using UnityEngine.UI;

public class HoldingFood : MonoBehaviour
{
    [SerializeField] private SnackVariable _currentHoldingFood;
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        if (_currentHoldingFood.Snack != null)
        {
            transform.localScale = Vector3.one * 0.6f;
            if (_image.sprite != _currentHoldingFood.Snack.SnackImage)
                _image.sprite = _currentHoldingFood.Snack.SnackImage;
        }
        else
            transform.localScale = Vector3.zero;
    }
}
