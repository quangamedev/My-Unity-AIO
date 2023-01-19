/*--------------------------------------
Unity All-in-One Project
+---------------------------------------
Author: Quan Nguyen
Date:   2/10/21
--------------------------------------*/

using System.Collections.Generic;
using DesignPatterns.Singleton;
using UnityEngine;

namespace DesignPatterns.ObjectPool
{
    /// <summary>
    /// This class is a pool that contains pools of other objects.
    /// This class can be used to create and pool any object.
    /// </summary>
    public class ObjectPoolBasic : Singleton<ObjectPoolBasic>
    {
        //Dictionary that contains many queues of objects (object pools)
        private Dictionary<string, Queue<GameObject>> _objectPool = new Dictionary<string, Queue<GameObject>>();

        /// <summary>
        /// Gets a GameObject from the pool, can be used similarly to Instantiate.
        /// </summary>
        /// <param name="original">An existing object that you want to make a copy of.</param>
        /// <returns></returns>
        public GameObject GetGameObject(GameObject original)
        {
            if (_objectPool.TryGetValue(original.name, out Queue<GameObject> gameObjectQueue))
            {
                if (gameObjectQueue.Count == 0)
                    return CreateNewGameObject(original);
                else
                {
                    GameObject _original = gameObjectQueue.Dequeue();
                    _original.SetActive(true);
                    return _original;
                }
            }
            else
                return CreateNewGameObject(original);
        }

        /// <summary>
        /// Gets a GameObject from the pool, can be used similarly to Instantiate.
        /// </summary>
        /// <param name="original">An existing object that you want to make a copy of.</param>
        /// <returns></returns>
        public GameObject GetGameObject(GameObject original, Vector3 position, Quaternion rotation)
        {
            if (_objectPool.TryGetValue(original.name, out Queue<GameObject> gameObjectQueue))
            {
                if (gameObjectQueue.Count == 0)
                    return CreateNewGameObject(original, position, rotation);
                else
                {
                    GameObject _original = gameObjectQueue.Dequeue();
                    _original.transform.position = position;
                    _original.transform.rotation = rotation;
                    _original.SetActive(true);
                    return _original;
                }
            }
            else
                return CreateNewGameObject(original, position, rotation);
        }

        /// <summary>
        /// Instantiate a new GameObject and set its name.
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        private GameObject CreateNewGameObject(GameObject original)
        {
            GameObject newGO = Instantiate(original, Vector3.zero, Quaternion.identity);
            newGO.name = original.name;
            return newGO;
        }

        /// <summary>
        /// Instantiate a new GameObject, set its transform and name.
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        private GameObject CreateNewGameObject(GameObject original, Vector3 position, Quaternion rotation)
        {
            GameObject newGO = Instantiate(original, position, rotation);
            newGO.name = original.name;
            return newGO;
        }

        /// <summary>
        /// Return the GameObject to the dictionary.
        /// </summary>
        /// <param name="original"></param>
        public void ReturnGameObject(GameObject original)
        {
            if (_objectPool.TryGetValue(original.name, out Queue<GameObject> gameObjectQueue))
            {
                gameObjectQueue.Enqueue(original);
            }
            else
            {
                Queue<GameObject> newGameObjectQueue = new Queue<GameObject>();
                newGameObjectQueue.Enqueue(original);
                _objectPool.Add(original.name, newGameObjectQueue);
            }

            original.SetActive(false);
        }
    }
}