/*--------------------------------------
Unity All-in-One Project
+---------------------------------------
Author: Quan Nguyen
Date:   15/01/21
--------------------------------------*/

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DesignPatterns.ObjectPool;
using DesignPatterns.Singleton;

namespace DesignPatterns.ObjectPool
{
    /// <summary>
    /// Object pool that can return desired components' references without using GetComponent
    /// </summary>

    public class ObjectPool : LazySingleton<ObjectPool>
    {
        private ComponentPool componentPool = new ComponentPool();

        public void PreloadPool<T>(T prefabReference, int count = 1) where T : Component
        {
            componentPool.AddToPool(prefabReference, count);
        }
    
        public T Spawn<T>(T prefabReference, int count = 1) where T : Component
        {
            return componentPool.GetAvailableObject(prefabReference);
        }

        public void Recycle<T>(T objectReference) where T : Component
        {
            componentPool.ReturnObjectToPool(objectReference);
        }
    }
}

public class ComponentPool
{
    //the queue of pooled components by their type and asset reference
    private Dictionary<Type, Dictionary<GameObject ,Queue<Component>>> _pooledComponentsByType = new Dictionary<Type, Dictionary<GameObject, Queue<Component>>>();

    //dictionaries of instantied objects and their original object
    private Dictionary<GameObject, GameObject> _originalsByInstantiatedObjects = new Dictionary<GameObject, GameObject>();

    /// <summary>
    /// Add new objects to the pool.
    /// </summary>
    /// <param name="prefabReference">Referenced object</param>
    /// <param name="count">Number of objects</param>
    /// <typeparam name="T">Type reference of the object</typeparam>
    /// <returns></returns>
    public Queue<Component> AddToPool<T>(T prefabReference, int count = 1) where T : Component
    {
        Type compType = prefabReference.GetType();

        if (count <= 0)
        {
            Debug.LogError("Count cannot be <= 0");
            return null;
        }

        Queue<Component> components;

        if (_pooledComponentsByType.TryGetValue(compType, out var componentsByPrefab))
        {
            //Check if the component type already exist in the Dictionary
            if (!componentsByPrefab.TryGetValue(prefabReference.gameObject, out components))
            {
                componentsByPrefab.Add(prefabReference.gameObject, components = new Queue<Component>(count));
            }
        }
        else
        {
            componentsByPrefab = new Dictionary<GameObject, Queue<Component>>
                {{prefabReference.gameObject, components = new Queue<Component>(count)}};
            _pooledComponentsByType.Add(compType, componentsByPrefab);
        }
        
        //Create the type of component x times
        for (int i = 0; i < count; i++)
        {
            //Instantiate new component and UPDATE the List of components
            Component instance = GameObject.Instantiate(prefabReference);
            _originalsByInstantiatedObjects.Add(instance.gameObject,prefabReference.gameObject);
            //De-activate each one until when needed
            instance.gameObject.SetActive(false);
            components.Enqueue(instance);
        }

        return components;
    }


    //Get available component in the ComponentPool
    public T GetAvailableObject<T>(T prefabReference) where T : Component
    {
        Type compType = prefabReference.GetType();
        
        //Get all component with the requested type from  the Dictionary
        if (_pooledComponentsByType.TryGetValue(compType, out Dictionary<GameObject, Queue<Component>> componentsByPrefab)
            && componentsByPrefab.TryGetValue(prefabReference.gameObject, out Queue<Component> components))
        {
            if (components.Count > 0)
            {
                var component = components.Dequeue();
                component.gameObject.SetActive(true);
                return (T)component;
            }
        }

        //No available object in the pool. Expand list
        //Create new component, activate the GameObject and return it
        Component instance = AddToPool(prefabReference).Dequeue();
        instance.gameObject.SetActive(true);
        return (T) instance;
    }

    public void ReturnObjectToPool<T>(T objectReference) where T : Component
    {
        Type compType = objectReference.GetType();
        Queue<Component> components;
        
        GameObject obj = objectReference.gameObject;
        obj.transform.position = Vector3.zero;
        obj.transform.rotation = Quaternion.identity;
        objectReference.gameObject.SetActive(false);

        GameObject original;
        if (!_originalsByInstantiatedObjects.TryGetValue(objectReference.gameObject, out original))
        {
            original = objectReference.gameObject;
            _originalsByInstantiatedObjects.Add(original, original);
        }
        
        if (_pooledComponentsByType.TryGetValue(compType, out var componentsByPrefab))
        {
            //Check if the component type already exist in the Dictionary
            if (!componentsByPrefab.TryGetValue(original, out components))
            {
                componentsByPrefab.Add(original, components = new Queue<Component>());
            }
        }
        else
        {
            componentsByPrefab = new Dictionary<GameObject, Queue<Component>>
                {{objectReference.gameObject, components = new Queue<Component>()}};
            _pooledComponentsByType.Add(compType, componentsByPrefab);
        }
        
        components.Enqueue(objectReference);
    }
}

public static class ObjectPoolExtension
{
    
    public static void CreatePool(this Component component, int count = 1)
    {
        ObjectPool.Instance.PreloadPool(component, count);
    }
    public static void ReturnToPool(this Component component)
    {
        ObjectPool.Instance.Recycle(component);
    }
    
    public static T SpawnFromPool<T>(this Component component) where T : Component
    {
        return (T) ObjectPool.Instance.Spawn(component);
    }
}
