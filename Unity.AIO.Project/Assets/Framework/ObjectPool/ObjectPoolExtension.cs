using UnityEngine;

public static class ObjectPoolExtension
{
    //extensions for components
    public static void AddPooled(this Component component, int count = 1)
    {
        ObjectPool.Instance.AddPooled(component, count);
    }

    public static void ReturnToPool(this Component component)
    {
        ObjectPool.Instance.Recycle(component);
    }

    public static void ReturnToPool(this Component component, float delay)
    {
        ObjectPool.Instance.Recycle(component, delay);
    }

    public static T SpawnFromPool<T>(this T component) where T : Component
    {
        return ObjectPool.Instance.Spawn(component);
    }

    //extensions for game objects as game objects inherits directly from Object instead of Component
    //not recommended to use game objects with this pool system, use Transform instead
    public static void AddPooled(this GameObject gameObject, int count = 1)
    {
        ObjectPool.Instance.AddPooled(gameObject.transform, count);
    }

    public static void ReturnToPool(this GameObject gameObject)
    {
        ObjectPool.Instance.Recycle(gameObject.transform);
    }

    public static void ReturnToPool(this GameObject gameObject, float delay)
    {
        ObjectPool.Instance.Recycle(gameObject.transform, delay);
    }

    public static GameObject SpawnFromPool(this GameObject gameObject)
    {
        return ObjectPool.Instance.Spawn(gameObject.transform).gameObject;
    }
}