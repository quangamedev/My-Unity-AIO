/*--------------------------------------
Unity All-in-One Project
+---------------------------------------
Author: Quan Nguyen
Date:   3/3/22
--------------------------------------*/

namespace DesignPatterns.Observer.ScriptableObjectsEventSystem
{
public interface IGameEventListener<T>
{
    void OnEventRaised(T item);
}
    
}
