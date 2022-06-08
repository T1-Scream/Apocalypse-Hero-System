using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using Dialogue;

[CustomEditor(typeof(Dialog))]
public class DialogEditor : Editor
{
    private SerializedProperty talkOnlyField;
    private SerializedProperty choiceField;

    private ReorderableList talkOnlyList;
    private ReorderableList choiceList;

    private void OnEnable()
    {
        talkOnlyField = serializedObject.FindProperty("dialog");
        choiceField = serializedObject.FindProperty("choiceSetting");

        talkOnlyList = new ReorderableList(serializedObject, talkOnlyField, true, true, true, true);
        choiceList = new ReorderableList(serializedObject, choiceField, false, true, true, true);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawPropertiesExcluding(serializedObject, "dialog", "choiceSetting");
        Dialog script = target as Dialog;
        DrawMaxSentence(script);

        //draw enum
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Events", EditorStyles.boldLabel);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        script.dialogueType = (DialogueType)EditorGUILayout.EnumPopup("Dialogue Event", script.dialogueType);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Sentences: " + script.MaxSentences, EditorStyles.boldLabel);
        EditorGUILayout.EndHorizontal();

        var p = serializedObject.GetIterator();
        do {
            if (p.name == "continueBtn") { // draw in next property
                // draw dialog setting
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.BeginVertical();
                if (script.dialogueType == DialogueType.TalkOnly) {
                    SerializedProperty dialogField = serializedObject.FindProperty("dialog");
                    talkOnlyList.drawHeaderCallback = rect => { DrawHeader(rect,dialogField.displayName); };
                    talkOnlyList.drawElementCallback = DrawTalkListItem;
                    talkOnlyList.elementHeightCallback = TalkListElementHeight;
                    talkOnlyList.DoLayoutList();
                }

                if (script.dialogueType == DialogueType.TalkAndChoices) {
                    SerializedProperty dialogField = serializedObject.FindProperty("choiceSetting");
                    choiceList.drawHeaderCallback = rect => { DrawHeader(rect, dialogField.displayName); };
                    choiceList.drawElementCallback = DrawChoiceListItem;
                    choiceList.elementHeightCallback = ChoiceListElementHeight;
                    choiceList.DoLayoutList();
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndHorizontal();
            }
        }
        while (p.NextVisible(true));
        serializedObject.ApplyModifiedProperties();
    }

    private void DrawHeader(Rect rect, string name)
    {
        EditorGUI.LabelField(rect, name);
    }

    private float TalkListElementHeight(int index)
    {
        var element = talkOnlyList.serializedProperty.GetArrayElementAtIndex(index);

        return EditorGUI.GetPropertyHeight(element) + EditorGUIUtility.standardVerticalSpacing;
    }

    private float ChoiceListElementHeight(int index)
    {
        var element = choiceList.serializedProperty.GetArrayElementAtIndex(index);

        return EditorGUI.GetPropertyHeight(element) + EditorGUIUtility.standardVerticalSpacing;
    }

    private void DrawTalkListItem(Rect rect, int index, bool isActive, bool isFocused)
    {
        var element = talkOnlyList.serializedProperty.GetArrayElementAtIndex(index);

        EditorGUI.indentLevel++;
        EditorGUI.PropertyField(rect, element, includeChildren: true);
        EditorGUI.indentLevel--;
    }

    private void DrawChoiceListItem(Rect rect, int index, bool isActive, bool isFocused)
    {
        var element = choiceList.serializedProperty.GetArrayElementAtIndex(index);

        EditorGUI.indentLevel++;
        EditorGUI.PropertyField(rect, element, includeChildren: true);
        EditorGUI.indentLevel--;
    }

    private void DrawMaxSentence(Dialog dialog)
    {
        dialog.MaxSentences = 0;
        for (int i = 0; i < dialog.dialog.Length; i++)
            dialog.MaxSentences += dialog.dialog[i].sentence.Length;
    }
}
