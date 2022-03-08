using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventTestScript : MonoBehaviour
{
    public GameObjectGameEvent Event;

    private void OnTriggerEnter(Collider other)
    {
        Event.Raise(other.gameObject);
    }

}
