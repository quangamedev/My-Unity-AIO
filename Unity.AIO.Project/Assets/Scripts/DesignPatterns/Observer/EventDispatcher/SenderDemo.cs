/*--------------------------------------
Unity All-in-One Project
+---------------------------------------
Author: Quan Nguyen
Date:   2/10/21
--------------------------------------*/

using UnityEngine;

namespace DesignPatterns.Observer.EventDispatcher
{
    /// <summary>
    /// 
    /// </summary>
    public class SenderDemo : MonoBehaviour
    {
        #region Fields

        #endregion

        #region Unity Methods
        void Start()
        {

        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                this.PostEvent(EventID.OnDebug);
            }
        }
        #endregion
    }
}
