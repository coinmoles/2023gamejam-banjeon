using ScriptableObjectVariable;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NpcAggro/Work")]
public class WorkSO : ScriptableObject
{
    public string WorkName;
    public Vector2 WorkPosition;
    public float EndTime = 0;
    public FloatReference AggroTimeAdded;
    public FloatReference MaxAggroTime;
    public List<NpcWork> npcWorkListeners = new List<NpcWork>();
    public Sprite WorkImage;

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

    public void Sabotaged()
    {
        if (EndTime < Time.time)
        {
            EndTime = Mathf.Clamp(Time.time + AggroTimeAdded, 0, Time.time + MaxAggroTime);
            foreach (NpcWork npcWork in npcWorkListeners)
            {
                npcWork.OnWorkSabotaged();
            }
        }
        else
            EndTime = Mathf.Clamp(EndTime + AggroTimeAdded, 0, Time.time + MaxAggroTime);
    }

    public void Ended()
    {
        foreach (NpcWork npcWork in npcWorkListeners)
        {
            npcWork.OnWorkEnded();
        }
    }
}
