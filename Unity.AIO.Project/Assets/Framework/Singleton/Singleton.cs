using UnityEngine;


/// <summary>
/// Basic Singleton Class. Make any class inherit from this one to turn it into a Singleton.
/// Prevents duplicates.
/// This class must be added onto an object.
/// No extra features.
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    //the private instance of the singleton
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance) return _instance;

            //find all instances in the scene of the same type
            _instance = FindObjectOfType<T>();

            return _instance;
        }
    }

    //if the derived class calls Awake(), the below code will not run
    //therefor it is virtual so it can be overriden
    protected virtual void Awake()
    {
        //if the instance already exists, destroy it to prevent duplicates
        if (!_instance)
            _instance = this as T;
        else
            Destroy(gameObject);
    }
}