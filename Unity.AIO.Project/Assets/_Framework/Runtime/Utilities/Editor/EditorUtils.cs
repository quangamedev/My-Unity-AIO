#if UNITY_EDITOR
using System.Reflection;
using UnityEditor;

public static class EditorUtils
{
    public static void ClearEditorLogs()
    {
        var assembly = Assembly.GetAssembly(typeof(Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method?.Invoke(new object(), null);
    }
}
#endif
