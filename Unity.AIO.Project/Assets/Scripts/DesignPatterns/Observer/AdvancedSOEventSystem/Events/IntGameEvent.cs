/*--------------------------------------
Unity All-in-One Project
+---------------------------------------
Author: Quan Nguyen
Date:   4/3/22
--------------------------------------*/

using DesignPatterns.Observer.ScriptableObjectsEventSystem.Advanced.EventParameters;
using UnityEngine;

namespace DesignPatterns.Observer.ScriptableObjectsEventSystem.Advanced
{
    /// <summary>
    /// Void game event types with no parameters (the SO itself) inheriting from the Base Game Event
    /// </summary>
    [CreateAssetMenu(fileName = "New IntGameEvent", menuName = "Scriptable Objects/IntGameEvent", order = 0)]
    public class IntGameEvent : BaseGameEvent<int>
    {
    }
}
