/*--------------------------------------
Unity All-in-One Project
+---------------------------------------
Author: Quan Nguyen
Date:   2/10/21
--------------------------------------*/

using System;
using System.Collections.Generic;
using DesignPatterns.Singleton;
using UnityEngine;

namespace DesignPatterns.Observer.EventDispatcher
{
    /// <summary>
    /// The class that handles and help implement the observer pattern.
    /// </summary>
    public class EventDispatcher : Singleton<EventDispatcher>
    {
        #region Fields
        Dictionary<EventID, Action<object>> _listeners = new Dictionary<EventID, Action<object>>();
        bool _clearedAllListeners = false;
        #endregion

        #region Unity Methods
        void Start()
        {

        }

        void Update()
        {

        }

        private void OnDestroy()
        {
            ClearAllListeners();
        }
        #endregion

        #region Add Listeners, Post events, Remove listener

        /// <summary>
        /// Register to listen for eventID.
        /// </summary>
        /// <param name="eventID">EventID that object want to listen</param>
        /// <param name="callback">Callback will be invoked when this eventID be raised</para	m>
        public void RegisterListener(EventID eventID, Action<object> callback)
        {
            // checking params
            Debug.Assert(callback != null, "AddListener, event " + eventID.ToString() + ", callback = null!");
            Debug.Assert(eventID != EventID.None, "RegisterListener, event = None!");

            // check if listener exist in dictionary
            if (_listeners.ContainsKey(eventID))
            {
                // add callback to our collection
                _listeners[eventID] += callback;
            }
            else
            {
                // add new key-value pair
                _listeners.Add(eventID, null);
                _listeners[eventID] += callback;
            }
        }

        /// <summary>
        /// Posts the event. This will notify all listener that register for this event.
        /// </summary>
        /// <param name="eventID">EventID.</param>
        /// <param name="sender">Sender, in some cases, the Listener will need to know who sent this message.</param>
        /// <param name="param">Parameter. Can be anything (struct, class ...), Listener will make a cast to get the data</param>
        public void PostEvent(EventID eventID, object param = null)
        {
            if (!_listeners.ContainsKey(eventID))
            {
                Debug.Log("No listeners for this event: " + eventID);
                return;
            }

            // posting event
            var callbacks = _listeners[eventID];
            // if there's no listener remain, then do nothing
            if (callbacks != null)
            {
                callbacks(param);
            }
            else
            {
                Debug.Log("PostEvent " + eventID + " but no listener remain, Remove this key");
                _listeners.Remove(eventID);
            }
        }

        /// <summary>
        /// Removes the listener. Used to Unregister listener.
        /// </summary>
        /// <param name="eventID">EventID.</param>
        /// <param name="callback">Callback.</param>
        public void RemoveListener(EventID eventID, Action<object> callback)
        {
            // checking params
            Debug.Assert(callback != null, "RemoveListener, event " + eventID.ToString() + ", callback = null!");
            Debug.Assert(eventID != EventID.None, "AddListener, event = None!");

            if (_listeners.ContainsKey(eventID))
            {
                _listeners[eventID] -= callback;
            }
            else if (!_clearedAllListeners)
            {
                Debug.LogWarning("RemoveListener, not found key: " + eventID);
            }
        }

        /// <summary>
        /// Clears all the listener.
        /// </summary>
        public void ClearAllListeners()
        {
            _listeners.Clear();
            _clearedAllListeners = true;
        }
        #endregion
    }


    #region Extension class
    /// <summary>
    /// Shortcuts to make using the EventDispatcher easier.
    /// </summary>
    public static class EventDispatcherExtension
    {
        /// <summary>
        /// Use for registering with EventsManager.
        /// </summary>
        /// <param name="listener"></param>
        /// <param name="eventID"></param>
        /// <param name="callback"></param>
        public static void RegisterListener(this MonoBehaviour listener, EventID eventID, Action<object> callback)
        {
            EventDispatcher.Instance.RegisterListener(eventID, callback);
        }

        /// <summary>
        /// Post event with a parameter.
        /// </summary>
        /// <param name="listener"></param>
        /// <param name="eventID"></param>
        /// <param name="param"></param>
        public static void PostEvent(this MonoBehaviour listener, EventID eventID, object param)
        {
            EventDispatcher.Instance.PostEvent(eventID, param);
        }

        /// <summary>
        /// Post event with no parameters.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventID"></param>
        public static void PostEvent(this MonoBehaviour sender, EventID eventID)
        {
            EventDispatcher.Instance.PostEvent(eventID, null);
        }
    }
    #endregion
}