using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScreenNavigator.Runtime.Core.Modal;

namespace ThirdParty.Samples.UnityScreenNavigator
{
    public class UnityScreenNavigatorDemo : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            PushTestModal();
        }

        private void Update()
        {
            // Pushing P
            if (Input.GetKeyDown(KeyCode.P))
            {
                PushTestModal();
            }
        }

        [ContextMenu("Push Test Modal")]
        private void PushTestModal()
        {
            ModalContainer.Find("MainModalContainer").Push("BasicModal", true);
        }
    }
}