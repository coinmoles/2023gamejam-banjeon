using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ScriptableObjectVariable
{
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(FloatReference))]
    public class FloatReferenceDrawer : PropertyDrawer
    {
        /// <summary>
        /// Options to display in the popup to select constant or variable.
        /// </summary>
        private readonly string[] popupOptions =
            { "Use Constant", "Use Variable" };

        /// <summary> Cached style to use to draw the popup button. </summary>
        private GUIStyle popupStyle;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (popupStyle == null)
            {
                popupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"));
                popupStyle.imagePosition = ImagePosition.ImageOnly;
            }

            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);

            EditorGUI.BeginChangeCheck();

            // Get properties
            SerializedProperty useConstant = property.FindPropertyRelative("UseConstant");
            SerializedProperty constantValue = property.FindPropertyRelative("ConstantValue");
            SerializedProperty variable = property.FindPropertyRelative("Variable");

            // Calculate rect for configuration button
            Rect buttonRect = new Rect(position);
            buttonRect.yMin += popupStyle.margin.top;
            buttonRect.width = popupStyle.fixedWidth + popupStyle.margin.right;
            position.xMin = buttonRect.xMax;

            // Store old indent level and set it to 0, the PrefixLabel takes care of it
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            int result = EditorGUI.Popup(buttonRect, useConstant.boolValue ? 0 : 1, popupOptions, popupStyle);

            useConstant.boolValue = result == 0;

            if (useConstant.boolValue)
                EditorGUI.PropertyField(position, constantValue, GUIContent.none);

            if (!useConstant.boolValue)
            {
                Rect valueRect = new Rect(position.x, position.y, position.width * 1 / 3, position.height);
                Rect variableRect = new Rect(position.x + position.width * 10 / 27, position.y, position.width * 17 / 27, position.height);
                FloatVariable floatVariable = (FloatVariable)variable.objectReferenceValue;

                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.FloatField(valueRect, floatVariable != null ? floatVariable.Value : 0);
                EditorGUI.EndDisabledGroup();
                EditorGUI.PropertyField(variableRect, variable, GUIContent.none);
            }

            if (EditorGUI.EndChangeCheck())
                property.serializedObject.ApplyModifiedProperties();

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }
#endif
}