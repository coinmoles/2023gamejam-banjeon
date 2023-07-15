using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcAIStatic : NpcAI
{
    protected override FrameInput CalcDefaultInput()
    {
        return NO_INPUT;
    }
}