using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RuntimeSets<T> : ScriptableObject
{

    public List<T> items = new List<T>();

    public void Add(T item)
    {
        if (!items.Contains(item))
        {
            items.Add(item);
        }
    }
    public void Remove(T item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
        }
    }
}

[CreateAssetMenu(fileName = "GameObjectSets", menuName = "ScriptableObjects/GameObjectSets")]
public class GameObjectSets : RuntimeSets<GameObject>
{
}



