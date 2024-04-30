#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Diagnostics;
using System.Reflection;

public static class RuntimeEditorUtils
{
    [Conditional("UNITY_EDITOR")]
    public static void ClearEditorLogs()
    {
        var assembly = Assembly.GetAssembly(typeof(Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method?.Invoke(new object(), null);
    }
}
