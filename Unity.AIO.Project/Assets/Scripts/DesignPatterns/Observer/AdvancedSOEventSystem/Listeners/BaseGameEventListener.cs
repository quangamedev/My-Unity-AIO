/*--------------------------------------
Unity All-in-One Project
+---------------------------------------
Author: Quan Nguyen
Date:   3/3/22
--------------------------------------*/

using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public abstract class BaseGameEventListener<T, GE, UER> : MonoBehaviour, IGameEventListener<T>
    where GE : BaseGameEvent<T> where UER : UnityEvent<T>
{
    #region Fields

    [SerializeField] private GE _gameEvent;

    public GE GameEvent
    {
        get => _gameEvent;
        private set => _gameEvent = value;
    }

    [SerializeField] private UER _unityEventResponse;

    #endregion

    #region Unity Methods

    private void OnEnable()
    {
        if (!_gameEvent) return;

        GameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        if (!_gameEvent) return;

        GameEvent.UnregisterListener(this);
    }

    #endregion

    public void OnEventRaised(T item)
    {
        _unityEventResponse.Invoke(item);
    }
}