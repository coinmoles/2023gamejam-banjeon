using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticNpcAI : NpcAI
{
    protected override FrameInput CalcDefaultInput()
    {
        return NO_INPUT;
    }
}