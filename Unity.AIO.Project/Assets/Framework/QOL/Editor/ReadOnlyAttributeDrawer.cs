/*--------------------------------------
Reference: https://github.com/Comp3interactive
--------------------------------------*/

using UnityEngine;
using UnityEditor;

/// <summary>
/// Draws the property field for any field marked with ReadOnlyAttribute.
/// </summary>
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        using (new EditorGUI.DisabledScope(true))
        {
            EditorGUI.PropertyField(position, property, label, true);
        }
    }
}