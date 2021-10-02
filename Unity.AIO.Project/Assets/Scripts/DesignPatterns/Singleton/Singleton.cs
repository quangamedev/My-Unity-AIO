/*--------------------------------------
Unity All-in-One Project
+---------------------------------------
Author: Quan Nguyen
Date:   2/10/21
--------------------------------------*/

using UnityEngine;

namespace DesignPatterns.Singleton
{
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
                if (_instance != null) return _instance;
                
                //find all instances in the scene of the same type
                _instance = GameObject.FindObjectOfType<T>();
                
                return _instance;
            }
        }

        //if the derived class calls Awake(), the below code will not run
        //therefor it is virtual so it can be overriden
        protected virtual void Awake()
        {
            //if the instance already exists, destroy it to prevent duplicates
            if (_instance != null)
                Destroy(this.gameObject);
        }
        
    }
}