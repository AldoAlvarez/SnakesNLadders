using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace AGAC.General.CustomEditors
{
    [CustomPropertyDrawer(typeof(RangedFloat))]

    public sealed class RangedFloatDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            SetVariables(property);

            position.height = 15;
            float lowerValue = LowerValue.floatValue;
            float upperValue = UpperValue.floatValue;

            lowerValue = (float)Decimal.Round((decimal)lowerValue, 2);
            upperValue = (float)Decimal.Round((decimal)upperValue, 2);

            EditorGUI.MinMaxSlider(position, label, ref lowerValue, ref upperValue, MinValue.floatValue, MaxValue.floatValue);
            position.x += 30;
            position.width -= 30;
            position.y += 20;
            lowerValue = EditorGUI.FloatField(position, new GUIContent("Lower Value"), lowerValue);
            if (lowerValue < MinValue.floatValue)
                lowerValue = MinValue.floatValue;

            position.y += 20;
            upperValue = EditorGUI.FloatField(position, new GUIContent("Upper Value"), upperValue);
            if (upperValue > MaxValue.floatValue)
                upperValue = MaxValue.floatValue;

            if (lowerValue > upperValue)
            {
                float temp = lowerValue;
                lowerValue = upperValue;
                upperValue = temp;
            }

            LowerValue.floatValue = lowerValue;
            UpperValue.floatValue = upperValue;
            EditorGUI.EndProperty();
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 65;
        }

        #region VARIABLES
        private SerializedProperty LowerValue;
        private SerializedProperty UpperValue;
        private SerializedProperty MinValue;
        private SerializedProperty MaxValue;
        #endregion

        #region PRIVATE METHODS
        private void SetVariables(SerializedProperty rangedValue) 
        {
            LowerValue = rangedValue.FindPropertyRelative("LowerValue");
            UpperValue = rangedValue.FindPropertyRelative("UpperValue");
            MinValue = rangedValue.FindPropertyRelative("MinValue");
            MaxValue = rangedValue.FindPropertyRelative("MaxValue");
        }
        #endregion
    }
}