using UnityEngine;

public class SnackItemCollision : MonoBehaviour
{
    [SerializeField] private Snack _snackToGive;
    [SerializeField] private SnackVariable _currentHoldingSnack;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (_currentHoldingSnack.Snack == null)
            {
                _currentHoldingSnack.ObtainSnack(_snackToGive);
                Destroy(gameObject);
            }
        }
    }
}
