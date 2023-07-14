using UnityEngine;

[CreateAssetMenu(menuName = "NpcAggro/NpcWork")]
public class NpcWork : ScriptableObject
{
    public string WorkName;
}

public struct WorkAndLoc
{
    public NpcWork work;
    public Vector2 location;
};