using UnityEngine;

[CreateAssetMenu()]
public class SnackVariable : ScriptableObject
{
    private Snack _snack;

    public Snack Snack => _snack;

    public void ObtainSnack(Snack snack)
    {
        _snack = snack;
    }

    public void UseSnack()
    {
        _snack = null;
    }
}
