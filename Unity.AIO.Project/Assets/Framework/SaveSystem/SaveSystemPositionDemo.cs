using UnityEngine;

namespace Framework.Samples.SaveSytem
{
    /// <summary>
    /// An example class that needs to have data saved.
    /// This class, as well as other classes that needs saving, must implement the ISaveable interface.
    /// </summary>
    public class SaveSystemPositionDemo : MonoBehaviour, ISaveable
    {
        public object SaveState()
        {
            return new SaveData() {position = transform.position};
        }

        public void LoadState(object state)
        {
            var saveData = (SaveData)state;
            transform.position = saveData.position;
        }

        /// <summary>
        /// This struct is used to define what to save
        /// </summary>
        [System.Serializable]
        private struct SaveData
        {
            public SerializableVector3 position;
        }
    }
}