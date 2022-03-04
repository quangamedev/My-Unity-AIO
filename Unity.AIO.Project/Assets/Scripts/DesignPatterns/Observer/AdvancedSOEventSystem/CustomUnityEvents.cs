/*--------------------------------------
Unity All-in-One Project
+---------------------------------------
Author: Quan Nguyen
Date:   4/3/22
File Commentary: This file will be used to make custom Unity Events (with type parameter) to be used in custom SO Game Events
--------------------------------------*/

using UnityEngine.Events;
using DesignPatterns.Observer.ScriptableObjectsEventSystem.Advanced.EventParameters;

namespace DesignPatterns.Observer.ScriptableObjectsEventSystem.Advanced.CustomUnityEvents
{
    [System.Serializable]
    public class VoidUnityEvent : UnityEvent<Void>
    {
    }
    
    [System.Serializable]
    public class IntUnityEvent : UnityEvent<int>
    {
    }
}