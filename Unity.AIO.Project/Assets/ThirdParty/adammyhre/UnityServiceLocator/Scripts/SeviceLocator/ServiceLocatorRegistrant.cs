using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityServiceLocator {
    public class ServiceLocatorRegistrant : MonoBehaviour {
        [SerializeField] private MonoBehaviour _serviceToRegister;
        private void Awake() {
            ServiceLocator.Global.Register(_serviceToRegister.GetType(), _serviceToRegister);
        }
    }
}