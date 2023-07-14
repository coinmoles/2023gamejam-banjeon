using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    public HashSet<GameEventListener> listeners = new HashSet<GameEventListener>();

    public void Raise(Component sender, object data)
    {
        foreach (GameEventListener listener in listeners)
        {
            listener.OnEventRaised(sender, data);
        }
    }
    
    public void RegisterListener(GameEventListener listener)
    {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener)
    {
        if(listeners.Contains(listener))
            listeners.Remove(listener);
    }
}
