/*--------------------------------------
Reference: https://gist.github.com/capnslipp/349c18283f2fea316369
--------------------------------------*/

using UnityEngine;
using UnityEditor;
using UnityEditor.UI;

[CanEditMultipleObjects, CustomEditor(typeof(NonDrawingGraphic), false)]
public class NonDrawingGraphicEditor : GraphicEditor
{
	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		EditorGUILayout.PropertyField(m_Script, new GUILayoutOption[0]);
		// skipping AppearanceControlsGUI
		RaycastControlsGUI();
		serializedObject.ApplyModifiedProperties();
	}
}