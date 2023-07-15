using ScriptableObjectVariable;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WorkObjectWorkHandler : MonoBehaviour
{
    [SerializeField] private WorkSO _linkedWork;

    [Header("Day Night Cycle")]
    [SerializeField] private BoolReference _isDay;
    [SerializeField] private FloatReference _dayNightStart;
    [SerializeField] private FloatReference _dayNightLength;

    private void Start()
    {
        _linkedWork.EndTime = 0;
        _linkedWork.WorkPosition = transform.position;
    }

    private void Update()
    {
        if (Time.time > _linkedWork.EndTime)
        {
            _linkedWork.Ended();
            enabled = false;
        }
    }

    public void WorkSabotaged()
    {
        enabled = true;
        _linkedWork.Sabotaged();
    }

    public void OnPlayerHit(Component sender, object data)
    {
        if (!_isDay)
        {
            float timeFromDayStart = Time.time - _dayNightStart;
            float timeTilDayEnd = _dayNightLength - timeFromDayStart;
            _linkedWork.EndTime -= timeTilDayEnd;
        }
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(WorkObjectWorkHandler), editorForChildClasses: true)]
public class WorkObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawDefaultInspector();
        WorkObjectWorkHandler workObject = (WorkObjectWorkHandler)target;

        GUI.enabled = Application.isPlaying;
        if (GUILayout.Button("Sabotage"))
        {
            workObject.WorkSabotaged();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif