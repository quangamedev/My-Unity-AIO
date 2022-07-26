/*--------------------------------------
Unity All-in-One Project
+---------------------------------------
Author: Quan Nguyen
Date:   26/7/21
--------------------------------------*/

using UnityEngine;
public class GameObjectWeightedRandomGenerator : WeightedRandomArrayComponent<GameObject>
{
#if UNITY_EDITOR
    [ContextMenu("Test")]
    private void Test()
    {
        QuanNguyenUtils.ClearEditorLogs();

        for (int i = 0; i < 10000; i++)
        {
            print(GetRandomItem().name);
        }
    }
#endif
}
