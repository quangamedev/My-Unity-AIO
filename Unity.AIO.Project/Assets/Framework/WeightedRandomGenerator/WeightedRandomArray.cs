using UnityEngine;
using System.Linq;

/// <summary>
/// Generic Weighted Random Generator that returns anything.
/// The generator can also return a null if intended
/// </summary>
/// <typeparam name="T">The Item that we want to get from the system.</typeparam>
[System.Serializable]
public class WeightedRandomArray<T>
{
    [System.Serializable]
    private struct Entry
    {
        public T item;
        public int weight;

        public Entry(T item, int weight)
        {
            this.item = item;
            this.weight = weight;
        }
    }

    //the list that contains all the drops
    [SerializeField] private Entry[] _entries;

    public int Length => _entries.Length;

    private int[] _cumulativeWeights;

    /// <summary>
    /// Gets a random object based on the weight of the object.
    /// </summary>
    /// <returns></returns>
    public T GetRandomItem()
    {
        CalculateCumulativeWeights();

        //the total weight of items
        int total = _entries.Sum(item => item.weight);

        //random number 
        int randomNumber = Random.Range(1, total + 1);

        for (int i = 0; i < _cumulativeWeights.Length; i++)
        {
            if (randomNumber <= _cumulativeWeights[i])
            {
                return _entries[i].item;
            }
        }

        Debug.LogWarning("Could not get random object.");
        return default;
    }

    private void CalculateCumulativeWeights()
    {
        _cumulativeWeights = new int[_entries.Length];

        for (int i = 0; i < _cumulativeWeights.Length; i++)
        {
            if (i == 0)
            {
                _cumulativeWeights[0] = _entries[0].weight;
                continue;
            }

            _cumulativeWeights[i] = _cumulativeWeights[i - 1] + _entries[i].weight;
        }
    }

    public float GetDropPercentage(int index)
    {
        if (_entries == null) return -1f;

        int total = _entries.Sum(item => item.weight);

        return (float) _entries[index].weight / total;
    }
}
