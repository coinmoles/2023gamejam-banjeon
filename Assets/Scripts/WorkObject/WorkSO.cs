using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NpcAggro/Work")]
public class WorkSO : ScriptableObject
{
    public string WorkName;
    public Vector2 WorkPosition;
    public float EndTime = 0;
    public float MaxDelay = 0;
    [HideInInspector] public List<NpcAggro> npcWorkListeners = new List<NpcAggro>();

    public void RegisterListener(NpcAggro listener)
    {
        if (!npcWorkListeners.Contains(listener))
            npcWorkListeners.Add(listener);
    }

    public void UnregisterListener(NpcAggro listener)
    {
        if (npcWorkListeners.Contains(listener))
            npcWorkListeners.Remove(listener);
    }

    public void Sabotaged(float delay)
    {
        if (EndTime < Time.time)
        {
            EndTime = Mathf.Clamp(Time.time + delay, 0, Time.time + MaxDelay);
            foreach (NpcAggro npcAggro in npcWorkListeners)
            {
                npcAggro.OnWorkSabotaged();
            }
        }
        else
            EndTime = Mathf.Clamp(EndTime + delay, 0, Time.time + MaxDelay);
    }

    public void Ended()
    {
        foreach (NpcAggro npcAggro in npcWorkListeners)
        {
            npcAggro.OnWorkEnded();
        }
    }
}
