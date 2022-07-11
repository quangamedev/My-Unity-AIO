/*--------------------------------------
Unity All-in-One Project
+---------------------------------------
Author: Quan Nguyen
Date:   11/7/21
--------------------------------------*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Systems.Cooldown
{
    /// <summary>
    /// Interface for classes that have cooldown abilities
    /// </summary>
    public interface IHasCooldown
    {
        string Id { get; }
        float CooldownDuration { get; }
    }
}