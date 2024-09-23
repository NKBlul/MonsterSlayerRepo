using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WeaponSO))]
public class WeaponSOEditor : Editor
{
    SerializedProperty weaponSprite;
    SerializedProperty weaponName;
    SerializedProperty weaponType;
    SerializedProperty weaponElements;
    SerializedProperty weaponDamage;
    SerializedProperty weaponDesc;

    private void OnEnable()
    {
        weaponSprite = serializedObject.FindProperty("weaponSprite");
        weaponName = serializedObject.FindProperty("weaponName");
        weaponType = serializedObject.FindProperty("weaponType");
        weaponElements = serializedObject.FindProperty("weaponElements");
        weaponDamage = serializedObject.FindProperty("weaponDamage");
        weaponDesc = serializedObject.FindProperty("weaponDesc");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Display the sprite with a custom preview
        EditorGUILayout.LabelField("Hero Sprite", EditorStyles.boldLabel);
        Sprite sprite = weaponSprite.objectReferenceValue as Sprite;
        if (sprite != null)
        {
            // Display the sprite preview
            Rect spriteRect = GUILayoutUtility.GetRect(100, 100, GUILayout.ExpandWidth(false));
            GUI.DrawTexture(spriteRect, sprite.texture, ScaleMode.ScaleToFit);
        }
        else
        {
            EditorGUILayout.HelpBox("No sprite assigned", MessageType.Warning);
        }
        EditorGUILayout.PropertyField(weaponName);
        EditorGUILayout.PropertyField(weaponType);
        EditorGUILayout.PropertyField(weaponElements);
        EditorGUILayout.PropertyField(weaponDamage);
        EditorGUILayout.PropertyField(weaponDesc);

        serializedObject.ApplyModifiedProperties();
    }
}
