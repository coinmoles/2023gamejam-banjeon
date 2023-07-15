using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class SnackItemSnackHandler : MonoBehaviour
{
    [SerializeField] private SnackSO _snackToGive;
    [SerializeField] private SnackVariable _currentHoldingSnack;
    [SerializeField] private GameEvent _onSFXPlay;

#if UNITY_EDITOR
    [Header("Debug")]
    public GameObject target;
#endif

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (_currentHoldingSnack.Snack == null)
            {
                _currentHoldingSnack.ObtainSnack(_snackToGive);
                _onSFXPlay.Raise(this, "item_obtained");
                Destroy(gameObject);
            }
        }
    }

#if UNITY_EDITOR
    public void GiveSnackToTarget()
    {
        target.GetComponent<NpcSnack>().GivenSnack(_snackToGive);
    }
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(SnackItemSnackHandler), editorForChildClasses: true)]
public class SnackItemCollisionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawDefaultInspector();
        SnackItemSnackHandler snackItemCollision = (SnackItemSnackHandler)target;

        GUI.enabled = Application.isPlaying;
        if (GUILayout.Button("Give this snack to target"))
        {
            snackItemCollision.GiveSnackToTarget();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif