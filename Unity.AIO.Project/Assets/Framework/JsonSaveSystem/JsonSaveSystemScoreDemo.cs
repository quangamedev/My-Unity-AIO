using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Framework.Samples.JsonSaveSystem
{
    /// <summary>
    /// An example class that needs to have data saved.
    /// This class, as well as other classes that needs saving, must implement the ISaveable interface.
    /// </summary>
    public class JsonSaveSystemScoreDemo : MonoBehaviour, IJsonSaveable
    {
        [SerializeField] private int _highScore = 100;
        [SerializeField] private int _bestTime = 200;

        /// <summary>
        /// This struct is used to define what to save
        /// </summary>
        [System.Serializable]
        private struct SaveData
        {
            public int highScore;
            public int bestTime;
        }

        public JToken CaptureAsJToken()
        {
            return JToken.FromObject(new SaveData() {highScore = _highScore, bestTime = _bestTime});
        }

        public void RestoreFromJToken(JToken state)
        {
            var saveData = state.ToObject<SaveData>();
            _highScore = saveData.highScore;
            _bestTime = saveData.bestTime;
        }
    }
}