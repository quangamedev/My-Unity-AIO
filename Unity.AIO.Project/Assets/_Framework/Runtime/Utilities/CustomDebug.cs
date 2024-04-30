using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public static class CustomDebug
{
    [Conditional("ENABLE_REGULAR_LOGS"), Conditional("UNITY_EDITOR")]
    public static void Log(this object loggingObject, string message)
    {
        if (loggingObject is Object loggingUnityObject)
        {
            Log(loggingUnityObject.GetType().ToString(), message, loggingUnityObject);
            return;
        }
        Log(loggingObject.GetType().ToString(), message);
    }

    public static void LogWarning(this object loggingObject, string message)
    {
        if (loggingObject is Object loggingUnityObject)
        {
            LogWarning(loggingUnityObject.GetType().ToString(), message, loggingUnityObject);
            return;
        }
        LogWarning(loggingObject.GetType().ToString(), message);
    }

    public static void LogError(this object loggingObject, string message)
    {
        if (loggingObject is Object loggingUnityObject)
        {
            LogError(loggingUnityObject.GetType().ToString(), message, loggingUnityObject);
            return;
        }
        LogError(loggingObject.GetType().ToString(), message);
    }

    [Conditional("ENABLE_REGULAR_LOGS"), Conditional("UNITY_EDITOR")]
    public static void Log(string tag, string message, Object context = null)
    {
        Log($"[{tag}] {message}", context);
    }

    public static void LogWarning(string tag, string message, Object context = null)
    {
        LogWarning($"[{tag}] {message}", context);
    }

    public static void LogError(string tag, string message, Object context = null)
    {
        LogError($"[{tag}] {message}", context);
    }

    [Conditional("ENABLE_REGULAR_LOGS"), Conditional("UNITY_EDITOR")]
    public static void Log(string message, Object context = null)
    {
        if (context)
        {
            Debug.Log(message, context);
            return;
        }
        Debug.Log(message);
    }

    public static void LogWarning(string message, Object context = null)
    {
        if (context)
        {
            Debug.LogWarning(message, context);
            return;
        }
        Debug.LogWarning(message);
    }

    public static void LogError(string message, Object context = null)
    {
        if (context)
        {
            Debug.LogError(message, context);
            return;
        }
        Debug.LogError(message);
    }
}