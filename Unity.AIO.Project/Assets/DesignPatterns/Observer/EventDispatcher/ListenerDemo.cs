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
    public class ListenerDemo : MonoBehaviour
    {
        #region Fields

        #endregion

        #region Unity Methods
        void Start()
        {
            EventDispatcher.Instance.RegisterListener(EventID.OnDebug, (param) => Listen());
        }

        void Update()
        {

        }

        private void OnDisable()
        {
            EventDispatcher.Instance.RemoveListener(EventID.OnDebug, (param) => Listen());
        }
        #endregion

        void Listen()
        {
            Debug.Log("Event Dispatcher success");
        }
    }
}
