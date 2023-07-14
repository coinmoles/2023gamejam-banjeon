using System;

namespace ScriptableObjectVariable
{
    [Serializable]
    public class IntReference
    {
        public bool UseConstant = true;
        public int ConstantValue;
        public IntVariable Variable;

        public IntReference()
        { }

        public IntReference(int value)
        {
            UseConstant = true;
            ConstantValue = value;
        }

        public int Value
        {
            get { return UseConstant ? ConstantValue : Variable.Value; }
        }

        public static implicit operator int(IntReference reference)
        {
            return reference.Value;
        }

        public void SetValue(int value)
        {
            if (UseConstant)
                ConstantValue = value;
            else
                Variable.SetValue(value);
        }

        public void SetValue(IntVariable value)
        {
            if (UseConstant)
                ConstantValue = value.Value;
            else
                Variable.SetValue(value);
        }
    }
}