/*--------------------------------------
Unity All-in-One Project
+---------------------------------------
Author: Quan Nguyen
Date:   22/02/22
--------------------------------------*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A helper class that solves basic problems
/// </summary>
public static class QuanNguyenUtils
{
    public static bool IsInLayerMask(this int layer, LayerMask layerMask)
    {
        return layerMask == (layerMask | (1 << layer));
    }
}
