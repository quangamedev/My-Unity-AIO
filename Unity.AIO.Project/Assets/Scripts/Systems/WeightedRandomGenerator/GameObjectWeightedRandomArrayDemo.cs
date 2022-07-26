/*--------------------------------------
Unity All-in-One Project
+---------------------------------------
Author: Quan Nguyen
Date:   25/7/21
--------------------------------------*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class GameObjectWeightedRandomArrayDemo : MonoBehaviour
{
    #region Fields

    [SerializeField] private WeightedRandomArray<GameObject> gameObjectWeightedRandomArray;

    [SerializeField] [Range(100, 10000)] private int _iterations = 100;

    #endregion


    [ContextMenu("Test")]
    private void Test()
    {
#if UNITY_EDITOR
        QuanNguyenUtils.ClearEditorLogs();
#endif

        for (int i = 0; i < _iterations; i++)
        {
            print(gameObjectWeightedRandomArray.GetRandomItem().name);
        }
    }
}