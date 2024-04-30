using UnityEngine;

public class LazySingleton<T> : MonoBehaviour where T : LazySingleton<T>
{
    private static T s_instance;

    public static T Instance
    {
        get
        {
            if (s_instance) return s_instance;

            s_instance = FindObjectOfType<T>();

            if (!s_instance)
                s_instance = new GameObject(typeof(T) + " (lazy instantiated singleton)").AddComponent<T>();

            return s_instance;
        }
    }

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