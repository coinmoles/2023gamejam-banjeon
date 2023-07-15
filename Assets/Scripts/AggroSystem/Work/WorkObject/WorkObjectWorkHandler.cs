using ScriptableObjectVariable;
using System.Collections;
using UnityEditor;
using UnityEngine;

public enum WorkObjectType
{
    Metal,
    Wooden
};

public class WorkObjectWorkHandler : MonoBehaviour, IAggroSlidable
{
    [SerializeField] private WorkSO _linkedWork;
    [SerializeField] private WorkObjectType _workObjectType;

    [Header("Day Night Cycle")]
    [SerializeField] private BoolReference _isDay;
    [SerializeField] private FloatReference _dayNightStart;
    [SerializeField] private FloatReference _dayNightLength;

    [Header("Game Events")]
    [SerializeField] private GameEvent _onPlaySFX;


    public bool IsAggro => _linkedWork.EndTime > Time.time;
    public float MaxAggroTime => _linkedWork.MaxAggroTime;
    public float AggroEndTime => _linkedWork.EndTime;

    private bool _endFlag;

    private void Start()
    {
        _linkedWork.EndTime = 0;
        _linkedWork.WorkPosition = transform.position;
    }

    private void Update()
    {
        if (!_endFlag && Time.time > _linkedWork.EndTime)
        {
            _linkedWork.Ended();
            _endFlag = true;
        }
    }

    private IEnumerator PlaySabotageSFX()
    {
        _onPlaySFX.Raise(this, "swing");
        yield return new WaitForSeconds(0.5f);
        if (_workObjectType == WorkObjectType.Metal)
            _onPlaySFX.Raise(this, "metal_break");
        else if (_workObjectType == WorkObjectType.Wooden)
            _onPlaySFX.Raise(this, "wood_break");
    }

    public void WorkSabotaged()
    {
        _endFlag = false;
        StartCoroutine(PlaySabotageSFX());
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