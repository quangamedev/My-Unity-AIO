using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T s_instance;

    public static T Instance
    {
        get
        {
            if (s_instance) return s_instance;

            s_instance = FindObjectOfType<T>();

            return s_instance;
        }
    }

    /// <summary>
    /// If instance is a duplicate and to be destroyed at end of frame.
    /// Useful to break out of execution in overridden Awake methods
    /// </summary>
    protected bool ToBeDestroyed { get; private set; }

    protected virtual void Awake()
    {
        if (!s_instance)
        {
            s_instance = (T) this;
        }
        else
        {
            ToBeDestroyed = true;
            Debug.LogError("A Singleton instance of " + typeof(T) + " already exists.");
            Destroy(gameObject);
        }
    }
}