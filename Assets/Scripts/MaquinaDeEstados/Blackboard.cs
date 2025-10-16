using System.Collections.Generic;
using UnityEngine;

public class Blackboard
{
    private Dictionary<int, object> data = new Dictionary<int, object>();

    public void Set<T>(int key, T value)
    {
        data.TryAdd(key, value);
    }

    public T Get<T>(int key)
    {
        if(data.TryGetValue(key, out object value))
            return (T)value;
        return default(T);
    }
}
