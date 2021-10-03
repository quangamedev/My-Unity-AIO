/*--------------------------------------
Unity All-in-One Project
+---------------------------------------
Author: Quan Nguyen
Date:   3/10/21
--------------------------------------*/

using System;
using UnityEngine;

namespace Systems.SaveSystem
{
    /// <summary>
    /// An example class that needs to have data saved.
    /// This class, as well as other classes that needs saving, must implement the ISaveable interface.
    /// </summary>
    public class SaveSystemPositionDemo : MonoBehaviour, ISaveable
    {
        public object SaveState()
        {
            object position;
            return new SaveData()
            {
                posX = transform.position.x,
                posY = transform.position.y,
                posZ = transform.position.z
            };
        }

        public void LoadState(object state)
        {
            var saveData = (SaveData) state;
            transform.position = new Vector3(saveData.posX, saveData.posY, saveData.posZ);
        }
        
        /// <summary>
        /// This struct is used to define what to save
        /// </summary>
        [System.Serializable]
        private struct SaveData
        {
            public float posX, posY, posZ;
        }
    }
}
