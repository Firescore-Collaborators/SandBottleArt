using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public abstract class GameEventResponse<GameEvent, EventResponse> 
{
    public GameEvent Event;
    public EventResponse response;
}

[System.Serializable]
public abstract class GameEvent<ObjectType,GameEventResponse> : ScriptableObject
{
    GameEventResponse responses;

    public void Raise(ObjectType obj)
    {
        ((UnityEvent<ObjectType>)(object)responses).Invoke(obj);
    }

    public void RegisterListener(UnityAction<ObjectType> listener)
    {
        ((UnityEvent<ObjectType>)(object)responses).AddListener(listener);
    }

    public void UnRegisterListener(UnityAction<ObjectType> listener)
    {
        ((UnityEvent<ObjectType>)(object)responses).RemoveListener(listener);
    }
}

[System.Serializable]
public class GameObjectEvent:UnityEvent<GameObject>
{
}

[System.Serializable]
public class GameObjectGameEventResponse : GameEventResponse<GameObjectGameEvent, GameObjectEvent>
{
}

[CreateAssetMenu(fileName = "GameObjectGameEvent", menuName = "ScriptableObjects/GameObjectGameEvent")]
public class GameObjectGameEvent:GameEvent<GameObject, GameObjectEvent>
{
}


