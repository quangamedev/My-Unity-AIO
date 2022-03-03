/*--------------------------------------
Unity All-in-One Project
+---------------------------------------
Author: Quan Nguyen
Reference: https://forum.unity.com/threads/editor-tool-better-scriptableobject-inspector-editing.484393/
Date:   13/11/21
--------------------------------------*/

using UnityEngine;
 
/// <summary>
/// Use this property on a ScriptableObject type to allow the editors drawing the field to draw an expandable
/// area that allows for changing the values on the object without having to change editor.
/// </summary>
public class ExpandedScriptableObjectAttribute : PropertyAttribute
{
    public ExpandedScriptableObjectAttribute()
    {
 
    }
}