using UnityEngine;

/// <summary>
/// Basic Singleton class. Make any class inherit from this one to turn it into a Singleton.
/// An Object will be created upon instance getter if the the instance is null (lazy instantiation).
/// Prevents duplicates
/// Not suitable for referencing assets when lazy instantiated.
/// </summary>
public class LazySingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    //the private instance with lazy instantiation
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance) return _instance;

            //find all instances in the scene of the same type
            _instance = FindObjectOfType<T>();

            //If there is still no instance found, create a new object with the wanted class
            if (!_instance)
                _instance = new GameObject("Lazy Singleton (instance) of " + typeof(T)).AddComponent<T>();

            return _instance;
        }
    }

    //if the derived class implements Awake(), the below code will not run
    //therefore it is virtual so it can be overriden
    protected virtual void Awake()
    {
        //if the instance already exists, destroy it to prevent duplicates
        if (!_instance)
            _instance = this as T;
        else
            Destroy(gameObject);
    }
}