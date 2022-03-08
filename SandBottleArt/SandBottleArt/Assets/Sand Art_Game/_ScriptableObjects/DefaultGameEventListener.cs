using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DefaultGameEventResponse
{
    public string name;
    public DefaultGameEvent Event;
    public UnityEvent response;
}

public class DefaultGameEventListener : MonoBehaviour
{
    [SerializeField] DefaultGameEventResponse[] gameEventListeners;

    private void OnEnable()
    {

        foreach (DefaultGameEventResponse response in gameEventListeners)
        {
            response.Event.RegisterListener(response);
        }
    }

    private void OnDisable()
    {
        foreach (DefaultGameEventResponse response in gameEventListeners)
        {
            response.Event.UnregisterListener(response);
        }
    }
}
