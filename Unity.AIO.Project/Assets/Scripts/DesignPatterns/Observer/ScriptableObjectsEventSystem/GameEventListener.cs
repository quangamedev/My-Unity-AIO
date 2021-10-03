/*--------------------------------------
Unity All-in-One Project
+---------------------------------------
Author: Quan Nguyen
Date:   3/10/21
--------------------------------------*/

using System;
using UnityEngine;
using UnityEngine.Events;

namespace DesignPatterns.Observer.ScriptableObjectsEventSystem
{
    /// <summary>
    /// The listener that subscribes to ScriptableObjects events.
    /// Modify events that this class will listen to and its response in the Inspector.
    /// </summary>
    public class GameEventListener : MonoBehaviour
    {
        //ScriptableObject Event to register/subscribe to
        public GameEvent Event;

        //Response when the registered Event is raise
        public UnityEvent Response;

        //Subscribe to the ScriptableObject event
        private void OnEnable() => Event.RegisterListener(this);

        //Unsubscribe to the ScriptableObject event
        private void OnDisable() => Event.UnregisterListener(this);

        //Invokes all logics in the Responds
        public void OnEventRaised() => Response.Invoke();
    }
}
