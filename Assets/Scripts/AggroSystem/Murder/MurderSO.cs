using ScriptableObjectVariable;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NpcAggro/Murder")]
public class MurderSO : ScriptableObject
{
    public string WorkName;
    public Vector2 WorkPosition;
    public float EndTime = 0;
    public FloatReference AggroTimeAdded;
    public FloatReference MaxAggroTime;
    public Sprite MurderImage;

    public void Murdered()
    {
        if (EndTime < Time.time)
            EndTime = Mathf.Clamp(Time.time + AggroTimeAdded, 0, Time.time + MaxAggroTime);
        else
            EndTime = Mathf.Clamp(EndTime + AggroTimeAdded, 0, Time.time + MaxAggroTime);
    }
}
