/*--------------------------------------
Unity All-in-One Project
+---------------------------------------
Author: Quan Nguyen
Date:   15/01/21
--------------------------------------*/

using UnityEngine;
using System.Collections.Generic;
using DesignPatterns.ObjectPool;
using DesignPatterns.Singleton;
using Object = UnityEngine.Object;

namespace DesignPatterns.ObjectPool
{
    /// <summary>
    /// Object pool that can return desired components' references without using GetComponent
    /// </summary>

    public class ObjectPool : LazySingleton<ObjectPool>
    {
        private ComponentPool componentPool = new ComponentPool();

        public void PreloadPool<T>(T originalReference, int count = 1) where T : Component
        {
            componentPool.AddToPool(originalReference, count);
        }
    
        public T Spawn<T>(T originalReference) where T : Component
        {
            return componentPool.GetAvailableObject(originalReference);
        }

        public void Recycle<T>(T cloneReference) where T : Component
        {
            componentPool.ReturnCloneToPool(cloneReference);
        }
    }
}

public class ComponentPool
{
    //the queue of pooled components by their type and asset reference
    private Dictionary<GameObject ,Queue<Component>> _pooledComponentsByType = new Dictionary<GameObject ,Queue<Component>>();

    //dictionaries of instantied objects and their original object
    private Dictionary<GameObject, GameObject> _originalsByClones = new Dictionary<GameObject, GameObject>();

    /// <summary>
    /// Add new objects to the pool.
    /// </summary>
    /// <param name="originalReference">Referenced object</param>
    /// <param name="count">Number of objects</param>
    /// <typeparam name="T">Type reference of the object</typeparam>
    /// <returns></returns>
    public Queue<Component> AddToPool<T>(T originalReference, int count = 1) where T : Component
    {
        Queue<Component> components;

        if (!_pooledComponentsByType.TryGetValue(originalReference.gameObject, out components))
        {
            _pooledComponentsByType.Add(originalReference.gameObject, components = new Queue<Component>());
        }
        
        if (count < 0)
        {
            Debug.LogError("Count cannot be negative");
            return null;
        }

        //Create the type of component x times
        for (int i = 0; i < count; i++)
        {
            //Instantiate new component and UPDATE the List of components
            Component clone = Object.Instantiate(originalReference);
            _originalsByClones.Add(clone.gameObject,originalReference.gameObject);
            //De-activate each one until when needed
            clone.gameObject.SetActive(false);
            components.Enqueue(clone);
        }

        return components;
    }


    //Get available component in the ComponentPool
    public T GetAvailableObject<T>(T originalReference) where T : Component
    {
        //Get all component with the requested type from  the Dictionary
        if (_pooledComponentsByType.TryGetValue(originalReference.gameObject, out Queue<Component> components))
        {
            if (components.Count > 0)
            {
                var component = components.Dequeue();
                component.gameObject.SetActive(true);
                return (T) component;
            }
        }

        //No available object in the pool. Expand list
        //Create new component, activate the GameObject and return it
        Component clone = AddToPool(originalReference).Dequeue();
        clone.gameObject.SetActive(true);
        return (T) clone;
    }

    public void ReturnCloneToPool<T>(T cloneReference) where T : Component
    {
        Queue<Component> components;
        
        GameObject clone = cloneReference.gameObject;
        clone.transform.position = Vector3.zero;
        clone.transform.rotation = Quaternion.identity;
        clone.SetActive(false);

        GameObject original = GetOriginal(clone);
        
        if (!_pooledComponentsByType.TryGetValue(original, out components))
        {
            _pooledComponentsByType.Add(original, components = new Queue<Component>());
        }
        
        components.Enqueue(cloneReference);
    }

    private GameObject GetOriginal(GameObject clone)
    {
        if (_originalsByClones.TryGetValue(clone, out var original))
            return original;

        return SetOriginal(clone, clone);
    }
    
    private GameObject SetOriginal(GameObject clone, GameObject original)
    {
        if (!_originalsByClones.ContainsKey(clone))
        {
            _originalsByClones.Add(clone, original);
        }

        return original;
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
