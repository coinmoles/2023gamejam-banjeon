using UnityEngine;
using UnityEngine.UI;

public class WorkObjectWorkAggroSlider : MonoBehaviour
{
    private Slider _slider;

    private NpcSnack _npcSnack;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _npcSnack = GetComponentInParent<NpcSnack>();
    }

    private void Update()
    {
        if (_npcSnack.IsEating)
            _slider.value = (_npcSnack.AggroEndTime - Time.time) / _npcSnack.SnackAggroTime;
    }
}
