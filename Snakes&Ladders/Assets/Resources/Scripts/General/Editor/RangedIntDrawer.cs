using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace AGAC.General.CustomEditors
{
    [CustomPropertyDrawer(typeof(RangedInt))]
    public class RangedIntDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            SetVariables(property);

            position.height = 15;
            float lowerValue = LowerValue.intValue;
            float upperValue = UpperValue.intValue;

            lowerValue = (float)Decimal.Round((decimal)lowerValue, 0);
            upperValue = (float)Decimal.Round((decimal)upperValue, 0);

            EditorGUI.MinMaxSlider(position, label, ref lowerValue, ref upperValue, MinValue.intValue, MaxValue.intValue);
            position.x += 30;
            position.width -= 30;
            position.y += 20;
            lowerValue = EditorGUI.IntField(position, new GUIContent("Lower Value"), (int)lowerValue);
            if (lowerValue < MinValue.intValue)
                lowerValue = MinValue.intValue;

            position.y += 20;
            upperValue = EditorGUI.IntField(position, new GUIContent("Upper Value"), (int)upperValue);
            if (upperValue > MaxValue.intValue)
                upperValue = MaxValue.intValue;

            if (lowerValue > upperValue)
            {
                float temp = lowerValue;
                lowerValue = upperValue;
                upperValue = temp;
            }

            LowerValue.intValue = (int)lowerValue;
            UpperValue.intValue = (int)upperValue;

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