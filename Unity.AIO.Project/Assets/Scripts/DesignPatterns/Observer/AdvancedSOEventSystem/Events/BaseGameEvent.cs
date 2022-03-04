/*--------------------------------------
Unity All-in-One Project
+---------------------------------------
Author: Quan Nguyen
Date:   3/3/22
--------------------------------------*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public abstract class BaseGameEvent<T> : ScriptableObject
{
    #region Fields
    private List<IGameEventListener<T>> _eventListeners = new List<IGameEventListener<T>>();
    #endregion
    
    /// <summary>
    /// Sends a signal to all the subscribed Listeners
    /// </summary>
    public void Raise(T item)
    {
        //reverse for loop so the order of the indices will not be affected even if an object were to destroy itself
        for (int i = _eventListeners.Count - 1; i >= 0; i--)
            _eventListeners[i].OnEventRaised(item);
    }
    
    public void RegisterListener(IGameEventListener<T> listener)
    {
        if(!_eventListeners.Contains(listener))
            _eventListeners.Add(listener);
    }

    /// <summary>
    /// Removes a listener from the list if it is valid
    /// </summary>
    public void UnregisterListener(IGameEventListener<T> listener)
    {
        if (_eventListeners.Contains(listener))
            _eventListeners.Remove(listener);
    }
}
