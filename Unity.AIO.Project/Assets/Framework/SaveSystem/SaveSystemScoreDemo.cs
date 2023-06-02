using UnityEngine;

namespace Framework.Samples.SaveSystem
{
    /// <summary>
    /// An example class that needs to have data saved.
    /// This class, as well as other classes that needs saving, must implement the ISaveable interface.
    /// </summary>
    public class SaveSystemScoreDemo : MonoBehaviour, ISaveable
    {
        [SerializeField] private int _highScore = 100;
        [SerializeField] private int _bestTime = 200;

        public object SaveState()
        {
            return new SaveData() {highScore = _highScore, bestTime = _bestTime};
        }

        public void LoadState(object state)
        {
            var saveData = (SaveData)state;

            _highScore = saveData.highScore;
            _bestTime = saveData.bestTime;
        }

        /// <summary>
        /// This struct is used to define what to save
        /// </summary>
        [System.Serializable]
        private struct SaveData
        {
            public int highScore;
            public int bestTime;
        }
    }
}