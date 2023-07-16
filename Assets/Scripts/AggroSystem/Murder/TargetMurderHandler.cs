using ScriptableObjectVariable;
using System.Collections;
using UnityEngine;

public class TargetMurderHandler : MonoBehaviour, IAggroSlidable
{
    [SerializeField] private MurderSO _murder;

    [Header("Day Night Cycle")]
    [SerializeField] private BoolReference _isDay;
    [SerializeField] private FloatReference _dayNightStart;
    [SerializeField] private FloatReference _dayNightLength;

    [Header("Game Events")]
    [SerializeField] private GameEvent _onPlaySFX;

    [Header("Game Object")]
    [SerializeField] private GameObject _meatPrefab;

    public bool IsAggro => _murder.EndTime > Time.time;
    public float MaxAggroTime => _murder.MaxAggroTime;
    public float AggroEndTime => _murder.EndTime;
    public Sprite AggroSliderImage => _murder.MurderImage;

    private void Start()
    {
        _murder.EndTime = 0;
    }

    protected IEnumerator PlayMurderSFX()
    {
        _onPlaySFX.Raise(this, "swing");
        yield return new WaitForSeconds(0.5f);
        _onPlaySFX.Raise(this, "swing");
    }

    public void MurderTried()
    {
        StartCoroutine(PlayMurderSFX());
        _murder.Murdered();
    }

    public void OnPlayerHit(Component sender, object data)
    {
        if (!_isDay)
        {
            float timeFromDayStart = Time.time - _dayNightStart;
            float timeTilDayEnd = _dayNightLength - timeFromDayStart;
            _murder.EndTime -= timeTilDayEnd;
        }
    }

    public void OnGameClear()
    {
        Instantiate(_meatPrefab, transform.position, Quaternion.identity);
        Invoke("DestroyMethod", 0.1f);
    }

    private void DestroyMethod()
    {
        Destroy(gameObject);
    }
}
