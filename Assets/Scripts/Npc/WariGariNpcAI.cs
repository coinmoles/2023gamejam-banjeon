using ScriptableObjectVariable;
using System.Collections;
using UnityEngine;

public class WariGariNpcAI : NpcAI
{
    [SerializeField] private Vector2 inputVector;
    [SerializeField] private FloatReference _wariGariInterval;

    private void Start()
    {
        StartCoroutine(reverseMovement());
    }

    private IEnumerator reverseMovement()
    {
        while (true)
        {
            yield return new WaitForSeconds(_wariGariInterval);
            inputVector.x *= -1;
        }
    }

    protected override FrameInput CalcDefaultInput()
    {
        return new FrameInput()
        {
            Move = inputVector
        };
    }
}
