using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(CharacterController))]
public class AssistantCharacterInspector : Editor
{
    private CharacterController character;
    private GUIStyle activeStyle, completedStyle;
    void OnEnable()
    {
        character = (CharacterController)target;
        activeStyle = new GUIStyle();
        activeStyle.fontStyle = FontStyle.BoldAndItalic;
        activeStyle.normal.textColor = Color.green;
        activeStyle.onNormal.textColor = Color.green;
        activeStyle.focused.textColor = Color.green;
        activeStyle.onFocused.textColor = Color.green;
        completedStyle = new GUIStyle();
        completedStyle.fontStyle = FontStyle.Bold;
        completedStyle.normal.textColor = Color.red;
        completedStyle.onNormal.textColor = Color.red;
        completedStyle.focused.textColor = Color.red;
        completedStyle.onFocused.textColor = Color.red;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUILayout.LabelField("Command Queue:", EditorStyles.boldLabel);
        if (character.commands != null)
            for (int i = 0; i < character.commandLength; i++)
            {
                EditorGUILayout.BeginVertical(GUI.skin.box);
                if (character.commands[i] != null)
                    EditorGUILayout.LabelField(character.commands[i].name, EditorStyles.boldLabel);
                else if (i == (character.firstIndex - 1 + character.commandLength) % character.commandLength
                && character.activeCommand != null)
                {
                    if (!character.activeCommand.IsCompleted())
                        EditorGUILayout.LabelField(character.activeCommand.name, activeStyle);
                    else
                        EditorGUILayout.LabelField(character.activeCommand.name, completedStyle);
                }
                else
                    EditorGUILayout.LabelField("Null", EditorStyles.boldLabel);
                EditorGUILayout.EndVertical();
            }
        serializedObject.ApplyModifiedProperties();
    }
}