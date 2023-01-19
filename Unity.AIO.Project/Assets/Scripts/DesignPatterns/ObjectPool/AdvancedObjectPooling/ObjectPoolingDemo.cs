/*--------------------------------------
Unity All-in-One Project
+---------------------------------------
Author: Quan Nguyen
Date:   19/01/21
--------------------------------------*/

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace DesignPatterns.ObjectPool
{
    /// <summary>
    /// 
    /// </summary>
    public class ObjectPoolingDemo : MonoBehaviour
    {
        #region Fields

        [SerializeField] private float _spawnInterval = 0.5f;
        [SerializeField] private ReturnToPoolOnCollision pooledVariantA;
        [SerializeField] private ReturnToPoolOnCollision pooledVariantB;
        [SerializeField] private ReturnToPoolOnCollision pooledVariantC;

        private float _nextSpawnTime;
        #endregion

        #region Unity Methods

        private void Start()
        {
        }

        void Update()
        {
            if (Time.time > _nextSpawnTime)
            {
                var A = pooledVariantA.SpawnFromPool<ReturnToPoolOnCollision>();
                var B = pooledVariantB.SpawnFromPool<ReturnToPoolOnCollision>();
                var C = pooledVariantC.SpawnFromPool<ReturnToPoolOnCollision>();

                A.transform.position = new Vector3(getRandomInRange(), 10, getRandomInRange());
                B.transform.position = new Vector3(getRandomInRange(), 10, getRandomInRange());
                C.transform.position = new Vector3(getRandomInRange(), 10, getRandomInRange());
                
                _nextSpawnTime = Time.time + _spawnInterval;
            }

            float getRandomInRange()
            {
                return Random.Range(-8, 9);
            }
        }
        #endregion
    }
}