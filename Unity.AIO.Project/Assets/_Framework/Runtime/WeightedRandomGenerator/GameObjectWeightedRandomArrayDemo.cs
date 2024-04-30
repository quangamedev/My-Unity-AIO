using UnityEngine;

namespace Framework.Samples.WeightedRandom
{
    public class GameObjectWeightedRandomArrayDemo : MonoBehaviour
    {
        [SerializeField] [Range(100, 10000)] private int _iterations = 100;
        [SerializeField] private WeightedRandomArray<GameObject> gameObjectWeightedRandomArray;

        [ContextMenu("Test")]
        private void Test()
        {
            RuntimeEditorUtils.ClearEditorLogs();

            for (int i = 0; i < _iterations; i++)
            {
                print(gameObjectWeightedRandomArray.GetRandomItem().name);
            }
        }
    }
}