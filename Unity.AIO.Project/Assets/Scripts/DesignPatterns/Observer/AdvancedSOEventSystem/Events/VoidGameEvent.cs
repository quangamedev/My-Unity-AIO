/*--------------------------------------
Unity All-in-One Project
+---------------------------------------
Author: Quan Nguyen
Date:   4/3/22
--------------------------------------*/

using UnityEngine;

/// <summary>
/// Void game event types with no parameters (the SO itself) inheriting from the Base Game Event
/// </summary>
[CreateAssetMenu(fileName = "New VoidGameEvent", menuName = "Scriptable Objects/VoidGameEvent", order = 0)]
public class VoidGameEvent : BaseGameEvent<Void>
{
    public void Raise() => Raise(new Void());
}
