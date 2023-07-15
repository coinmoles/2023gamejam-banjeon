using ScriptableObjectVariable;
using System.Collections;
using UnityEngine;

public class NpcSnack : MonoBehaviour
{
    [Header("Aggro Data")]
    [SerializeField] private SnackSO _likedSnack;
    [SerializeField] private FloatReference _snackAggroTime;

    [Header("Aggro State")]
    [SerializeField] private bool _isEating;

    public bool IsEating => _isEating;

    #region Snack Functions
    private bool IsLikedSnack(SnackSO snack)
    {
        return snack == _likedSnack;
    }

    public void GivenSnack(SnackSO snack)
    {
        if (snack == _likedSnack && !_isEating)
        {
            _isEating = true;
            StartCoroutine(EndEating());
        }
    }
    private IEnumerator EndEating()
    {
        yield return new WaitForSeconds(_snackAggroTime);
        _isEating = false;
    }
    #endregion
}