using System;

namespace ScriptableObjectVariable
{
    [Serializable]
    public class BoolReference
    {
        public bool UseConstant = true;
        public bool ConstantValue;
        public BoolVariable Variable;

        public BoolReference()
        { }

        public BoolReference(bool value)
        {
            UseConstant = true;
            ConstantValue = value;
        }

        public bool Value
        {
            get { return UseConstant ? ConstantValue : Variable.Value; }
        }

        public static implicit operator bool(BoolReference reference)
        {
            return reference.Value;
        }

        public void SetValue(bool value)
        {
            if (UseConstant)
                ConstantValue = value;
            else
                Variable.SetValue(value);
        }

        public void SetValue(BoolVariable value)
        {
            if (UseConstant)
                ConstantValue = value.Value;
            else
                Variable.SetValue(value);
        }
    }
}