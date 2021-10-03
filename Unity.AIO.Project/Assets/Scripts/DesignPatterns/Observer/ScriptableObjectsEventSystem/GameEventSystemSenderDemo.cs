/*--------------------------------------
Unity All-in-One Project
+---------------------------------------
Author: Quan Nguyen
Date:   //21
--------------------------------------*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DesignPatterns.Observer.ScriptableObjectsEventSystem;

/// <summary>
/// 
/// </summary>
public class GameEventSystemSenderDemo : MonoBehaviour
{
    public GameEvent OnDeveloperTest;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) OnDeveloperTest.Raise();
    }
}
