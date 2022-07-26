/*--------------------------------------
Unity All-in-One Project
+---------------------------------------
Author: Quan Nguyen
Date:   11/7/22
--------------------------------------*/

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Systems.Cooldown;

/// <summary>
/// Demo implementation for the cooldown system
/// </summary>
public class CooldownImplementationDemo : MonoBehaviour, IHasCooldown
{
    #region Fields
    
    [Header("Cooldown settings")]
    [SerializeField] private string _id = string.Empty;
    [SerializeField] private float _cooldownDuration = 1f;

    public string Id => _id;
    public float CooldownDuration => _cooldownDuration;

    #endregion

    #region Unity Methods
    void Awake()
    {
        if (_id == string.Empty)
        {
            GenerateId();
        }
    }
 
    void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        
        if (CooldownSystem.Instance.IsOnCooldown(_id)) return;
        
        print("Fired");
        
        CooldownSystem.Instance.PutOnCooldown(this);
    }
    
    #endregion
    
    [ContextMenu("Generate Id")]
    private void GenerateId()
    {
        _id = Guid.NewGuid().ToString();
    }
}
