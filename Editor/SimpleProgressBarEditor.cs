using UnityEditor;

using UnityEngine;

[CustomEditor(typeof(SimpleProgressBar))]
public class SimpleProgressBarEditor : Editor
{
    SerializedProperty graphic;
    SerializedProperty outlineColor;
    SerializedProperty background;
    SerializedProperty backgroundColor;
    SerializedProperty outlineThickness;
    SerializedProperty gradient;
    SerializedProperty color;
    SerializedProperty minColor;
    SerializedProperty maxColor;
    SerializedProperty progressType;
    SerializedProperty fillType;
    SerializedProperty fillMethod;
    SerializedProperty min;
    SerializedProperty max;
    SerializedProperty size;
    SerializedProperty value;
    SerializedProperty showPercentage;
    SerializedProperty percentage;

    private SerializedObject serializedTarget;

    private void OnEnable()
    {
        graphic = serializedObject.FindProperty("graphic");
        outlineColor = serializedObject.FindProperty("outlineColor");
        backgroundColor = serializedObject.FindProperty("backgroundColor");
        background = serializedObject.FindProperty("background");
        outlineThickness = serializedObject.FindProperty("outlineThickness");
        gradient = serializedObject.FindProperty("gradient");
        color = serializedObject.FindProperty("color");
        minColor = serializedObject.FindProperty("minColor");
        maxColor = serializedObject.FindProperty("maxColor");
        progressType = serializedObject.FindProperty("progressType");
        fillType = serializedObject.FindProperty("fillType");
        fillMethod = serializedObject.FindProperty("fillMethod");
        min = serializedObject.FindProperty("min");
        max = serializedObject.FindProperty("max");
        size = serializedObject.FindProperty("size");
        value = serializedObject.FindProperty("value");
        percentage = serializedObject.FindProperty("percentage");
        showPercentage = serializedObject.FindProperty("showPercentage");
        serializedTarget = new SerializedObject(target);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        serializedTarget.Update();

        EditorGUILayout.PropertyField(graphic);

        if (graphic.objectReferenceValue == null)
        {
            EditorGUILayout.HelpBox("Graphic field is not assigned. Please assign a valid Image component.", MessageType.Warning);
        }
        else
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(progressType);
            EditorGUILayout.PropertyField(size);

            if (progressType.enumValueIndex == (int)SimpleProgressBar.ProgressType.ColorOnly ||
                progressType.enumValueIndex == (int)SimpleProgressBar.ProgressType.FillColor)
            {
                EditorGUILayout.PropertyField(fillType);
                if (fillType.enumValueIndex == (int)SimpleProgressBar.FillType.SingleColor)
                {
                    EditorGUILayout.PropertyField(color);
                }
                else if (fillType.enumValueIndex == (int)SimpleProgressBar.FillType.MinMaxColor)
                {
                    EditorGUILayout.PropertyField(minColor);
                    EditorGUILayout.PropertyField(maxColor);
                }
                else if (fillType.enumValueIndex == (int)SimpleProgressBar.FillType.Gradient)
                {
                    EditorGUILayout.PropertyField(gradient);
                }
            }
            if(fillType.enumValueIndex != (int)SimpleProgressBar.ProgressType.ColorOnly)
            {
                EditorGUILayout.PropertyField(fillMethod);
            }
            EditorGUI.indentLevel--;
            EditorGUILayout.PropertyField(background);
            if ((target as SimpleProgressBar).background)
            {
                EditorGUILayout.PropertyField(outlineColor);
                EditorGUILayout.PropertyField(backgroundColor);
                EditorGUILayout.PropertyField(outlineThickness);
            }

            EditorGUILayout.PropertyField(min);
            EditorGUILayout.PropertyField(max);

            EditorGUILayout.Slider(value, min.floatValue, max.floatValue);

            EditorGUILayout.PropertyField(showPercentage);
            if (showPercentage.boolValue)
            {
                EditorGUILayout.PropertyField(percentage);
            }
            if (serializedObject.ApplyModifiedProperties())
            {
                serializedTarget.ApplyModifiedProperties();
                (target as SimpleProgressBar).UpdateGraphic();
            }
        }
        if (GUILayout.Button("Reset"))
        {
            (target as SimpleProgressBar).Reset();
        }
        serializedTarget.ApplyModifiedProperties();
    }
}
