using UnityEngine;

public class SnackItemGive : MonoBehaviour
{
    [SerializeField] private Snack _snackToGive;
    [SerializeField] private Snack _currentSnack;
    [SerializeField] private GameEvent _onSnackObtained;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (_currentSnack != null)
                _onSnackObtained.Raise(this, _snackToGive);
        }
    }
}
