/*--------------------------------------
Unity All-in-One Project
+---------------------------------------
Author: Quan Nguyen
Date:   //21
--------------------------------------*/

using UnityEngine;
using System;
using DesignPatterns.Observer.ScriptableObjectsEventSystem.Advanced;

/// <summary>
/// 
/// </summary>
public class GameEventSystemSenderDemo : MonoBehaviour
{
    public IntGameEvent OnIntDeveloperTest;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) OnIntDeveloperTest.Raise(DateTime.Now.Minute);
    }
}
