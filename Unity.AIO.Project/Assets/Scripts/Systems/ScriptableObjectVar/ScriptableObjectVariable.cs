/*--------------------------------------
Unity All-in-One Project
+---------------------------------------
Author: Quan Nguyen
Date:   2/8/21
--------------------------------------*/

using UnityEngine;
using DesignPatterns.Observer.ScriptableObjectsEventSystem;

namespace Systems.ScriptableObjectVariable
{
    /// <summary>
    /// Use this SO to replace normal floats when usage and references of a float is in high demand.
    /// </summary>
    public class ScriptableObjectVariable<T> : ScriptableObject
    {
        [Tooltip("Value can be modified during runtime")] [SerializeField]
        private T _value;

        [SerializeField] private GameEvent[] OnValueChangeEvents;

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                RaiseEvents();
            }
        }

        private void RaiseEvents()
        {
            if (OnValueChangeEvents == null) return;

            foreach (var gameEvent in OnValueChangeEvents)
            {
                gameEvent.Raise();
            }
        }
    }
}