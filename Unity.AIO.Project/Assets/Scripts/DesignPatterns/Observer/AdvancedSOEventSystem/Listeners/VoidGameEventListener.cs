/*--------------------------------------
Unity All-in-One Project
+---------------------------------------
Author: Quan Nguyen
Date:   4/3/22
--------------------------------------*/

using DesignPatterns.Observer.ScriptableObjectsEventSystem.Advanced.CustomUnityEvents;
using DesignPatterns.Observer.ScriptableObjectsEventSystem.Advanced.EventParameters;

namespace DesignPatterns.Observer.ScriptableObjectsEventSystem.Advanced
{
    /// <summary>
    /// The listener for custom void game events (SO) based off custom void UnityEvents
    /// </summary>
    public class VoidGameEventListener : BaseGameEventListener<Void, VoidGameEvent, VoidUnityEvent>
    {
    }
}