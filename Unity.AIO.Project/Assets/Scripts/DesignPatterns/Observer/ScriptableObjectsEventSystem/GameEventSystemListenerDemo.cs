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
public class GameEventSystemListenerDemo : MonoBehaviour
{
    public void PrintNameAndMinuteToConsole(int date) => print(gameObject.name + " " + date);
}
