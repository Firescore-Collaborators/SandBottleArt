using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DGameEvent",menuName = "ScriptableObjects / DGameEvent")]
public class DefaultGameEvent : ScriptableObject
{
    List<DefaultGameEventResponse> responses = new List<DefaultGameEventResponse>();

    public void Raise()
    {
        for(int i = 0; i<responses.Count;i++)
        {
            responses[i].response?.Invoke();

        }
    }

    public void RaiseDelay(float delayTime)
    {
        Timer.Delay(delayTime, Raise);
    }

    public void RegisterListener(DefaultGameEventResponse listener)
    {
        responses.Add(listener);
    }

    public void UnregisterListener(DefaultGameEventResponse listener)
    {
        responses.Remove(listener);
    }
}
