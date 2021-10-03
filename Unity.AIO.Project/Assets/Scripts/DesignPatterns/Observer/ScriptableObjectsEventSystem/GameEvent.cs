/*--------------------------------------
Unity All-in-One Project
+---------------------------------------
Author: Quan Nguyen
Date:   3/10/21
--------------------------------------*/

using UnityEngine;
using System.Collections.Generic;

namespace DesignPatterns.Observer.ScriptableObjectsEventSystem
{
    /// <summary>
    /// Scriptable Object script for GameEvents with scriptable object workflow.
    /// </summary>
    [CreateAssetMenu(fileName = "New GameEvent", menuName = "Scriptable Objects/GameEvent", order = 0)]
    public class GameEvent : ScriptableObject
    {
        //The list containing all listeners of this Event
        private List<GameEventListener> _eventListeners = new List<GameEventListener>();

        /// <summary>
        /// Sends a signal to all the subscribed Listeners
        /// </summary>
        public void Raise()
        {
            for (int i = _eventListeners.Count - 1; i >= 0; i--)
                _eventListeners[i].OnEventRaised();
        }

        /// <summary>
        /// Adds a unique listener to the list
        /// </summary>
        public void RegisterListener(GameEventListener listener)
        {
            if(!_eventListeners.Contains(listener)) _eventListeners.Add(listener);
        }

        /// <summary>
        /// Removes a listener from the list if it is valid
        /// </summary>
        public void UnregisterListener(GameEventListener listener)
        {
            if (_eventListeners.Contains(listener)) _eventListeners.Remove((listener));
        }
    }
}
