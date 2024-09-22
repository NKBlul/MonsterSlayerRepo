using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CharacterStatsSO))]
public class CharacterStatsSOEditor : Editor
{
    SerializedProperty element;
    SerializedProperty names;
    SerializedProperty level;
    SerializedProperty health;
    SerializedProperty maxHealth;
    SerializedProperty attack;
    SerializedProperty defense;

    private void OnEnable()
    {
        element = serializedObject.FindProperty("element");
        names = serializedObject.FindProperty("names");
        level = serializedObject.FindProperty("level");
        health = serializedObject.FindProperty("health");
        maxHealth = serializedObject.FindProperty("maxHealth");
        attack = serializedObject.FindProperty("attack");
        defense = serializedObject.FindProperty("defense");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(element);
        EditorGUILayout.PropertyField(names);
        EditorGUILayout.PropertyField(level);
        EditorGUILayout.PropertyField(health);
        EditorGUILayout.PropertyField(maxHealth);
        EditorGUILayout.PropertyField(attack);
        EditorGUILayout.PropertyField(defense);

        serializedObject.ApplyModifiedProperties();
    }
}
