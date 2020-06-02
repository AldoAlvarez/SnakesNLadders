using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using AGAC.SnakesLadders.Board_Auxiliary;

namespace AGAC.SnakesLadders.CustomEditors
{
    [CustomEditor(typeof(ConnectionsSetting))]
    public class ConnectionsSettingEditor : Editor
    {
        private void OnEnable()
        {
            SetVariables();
        }
        public override void OnInspectorGUI()
        {
            serializedObject.UpdateIfRequiredOrScript();
            UpdateCreationTiles();

            GUILayout.Space(10);
            DrawBoardRequest();
            GUILayout.Space(12);
            if (!hasBoardSettings())
                DrawWarningBox();
            else
                DrawBoardProperties();
            serializedObject.ApplyModifiedProperties();
        }

        #region VARIABLES
        private SerializedProperty Board;
        private SerializedProperty CreationTiles;
        private SerializedProperty tilesToMove;
        private SerializedProperty maximumConnections;
        private SerializedProperty AccessTextures;

        private BoardSettings BoardSettings;
        private SerializedProperty MinValue;
        private SerializedProperty MaxValue;
        private SerializedProperty LowerValue;
        private SerializedProperty UpperValue;

        private bool hasNewBoardSettings = false;
        #endregion

        #region PRIVATE METHODS
        #region draw methods
        private void DrawBoardRequest()
        {
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(Board);
            if (EditorGUI.EndChangeCheck())
                hasNewBoardSettings = true;
        }
        private void DrawWarningBox()
        {
            EditorGUILayout.HelpBox(
                "A < BoardSetting > reference is needed in order to modify the Settings Values for the tiles connections.",
                MessageType.Info,
                true);
        }
        private void DrawBoardProperties()
        {
            EditorGUILayout.PropertyField(CreationTiles);
            EditorGUILayout.PropertyField(tilesToMove);
            EditorGUILayout.PropertyField(maximumConnections);
            EditorGUILayout.PropertyField(AccessTextures, true);
        }
        #endregion

        #region ranged int
        private void GetBoardSettings()
        {
            SerializedObject BoardSettingsObject = new SerializedObject(Board.objectReferenceValue);
            BoardSettings = (BoardSettings)BoardSettingsObject.targetObject;
        }
        private void SetRangedIntVariables()
        {
            MinValue = CreationTiles.FindPropertyRelative("MinValue");
            MaxValue = CreationTiles.FindPropertyRelative("MaxValue");
            LowerValue = CreationTiles.FindPropertyRelative("LowerValue");
            UpperValue = CreationTiles.FindPropertyRelative("UpperValue");
        }
        private void SetCreationTilesValues()
        {
            UpdateRangedIntLimits();
            SetRangedIntValues(0, MaxValue.intValue);
        }
        private void UpdateRangedIntLimits() 
        {
            int maxValue = 0;
            if (hasBoardSettings())
            {
                GetBoardSettings();
                maxValue = (int)BoardSettings.TotalTiles;
            }
            SetRangedIntLimits(0, maxValue);
        }
        private void SetRangedIntValues(int min, int max)
        {
            LowerValue.intValue = min;
            UpperValue.intValue = max;
        }
        private void SetRangedIntLimits(int min, int max)
        {
            MinValue.intValue = min;
            MaxValue.intValue = max;
        }
        #endregion

        private void UpdateCreationTiles() 
        {
            if (hasNewBoardSettings)
            {
                SetCreationTilesValues();
                hasNewBoardSettings = false;
            }
            else
                UpdateRangedIntLimits();
        }

        private bool hasBoardSettings() 
        {
            return Board.objectReferenceValue != null;
        }

        private void SetVariables()
        {
            Board = serializedObject.FindProperty("Board");
            CreationTiles = serializedObject.FindProperty("CreationTiles");
            tilesToMove = serializedObject.FindProperty("tilesToMove");
            maximumConnections = serializedObject.FindProperty("MaxConnections");
            AccessTextures = serializedObject.FindProperty("AccessTextures");

            SetRangedIntVariables();
        }
        #endregion
    }
}