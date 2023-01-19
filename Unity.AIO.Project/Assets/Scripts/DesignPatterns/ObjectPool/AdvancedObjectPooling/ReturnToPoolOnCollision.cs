/*--------------------------------------
Unity All-in-One Project
+---------------------------------------
Author: Quan Nguyen
Date:   //21
--------------------------------------*/

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DesignPatterns.ObjectPool
{
    /// <summary>
    /// 
    /// </summary>
    public class ReturnToPoolOnCollision : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            this.ReturnToPool();
        }
    }
}