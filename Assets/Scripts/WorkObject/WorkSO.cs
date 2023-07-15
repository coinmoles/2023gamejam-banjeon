using ScriptableObjectVariable;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NpcAggro/Work")]
public class WorkSO : ScriptableObject
{
    public string WorkName;
    public Vector2 WorkPosition;
    public float EndTime = 0;
    public FloatReference MaxDelay;
    public List<NpcWork> npcWorkListeners = new List<NpcWork>();

    public void RegisterListener(NpcWork listener)
    {
        if (!npcWorkListeners.Contains(listener))
            npcWorkListeners.Add(listener);
    }

    public void UnregisterListener(NpcWork listener)
    {
        if (npcWorkListeners.Contains(listener))
            npcWorkListeners.Remove(listener);
    }

    public void Sabotaged(float delay)
    {
        if (EndTime < Time.time)
        {
            EndTime = Mathf.Clamp(Time.time + delay, 0, Time.time + MaxDelay);
            foreach (NpcWork npcWork in npcWorkListeners)
            {
                npcWork.OnWorkSabotaged();
            }
        }
        else
            EndTime = Mathf.Clamp(EndTime + delay, 0, Time.time + MaxDelay);
    }

    public void Ended()
    {
        foreach (NpcWork npcWork in npcWorkListeners)
        {
            npcWork.OnWorkEnded();
        }
    }
}
