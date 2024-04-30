using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Framework.Samples.JsonSaveSystem
{
    /// <summary>
    /// An example class that needs to have data saved.
    /// This class, as well as other classes that needs saving, must implement the ISaveable interface.
    /// </summary>
    public class JsonSaveSystemPhysicsDemo : MonoBehaviour, IJsonSaveable
    {
        [SerializeField] private Rigidbody _rigidbody;

        /// <summary>
        /// This struct is used to define what to save
        /// </summary>
        [System.Serializable]
        private struct SaveData
        {
            public Vector3 position;
            public Quaternion rotation;
            public Vector3 velocity;
            public Vector3 angularVelocity;
        }

        public JToken CaptureAsJToken()
        {
            return JToken.FromObject(new SaveData()
            {
                position = transform.position,
                rotation = transform.rotation,
                velocity = _rigidbody.velocity,
                angularVelocity = _rigidbody.angularVelocity
            });
        }

        public void RestoreFromJToken(JToken state)
        {
            var saveData = state.ToObject<SaveData>();
            transform.position = saveData.position;
            transform.rotation = saveData.rotation;
            _rigidbody.velocity = saveData.velocity;
            _rigidbody.angularVelocity = saveData.angularVelocity;
        }
    }
}