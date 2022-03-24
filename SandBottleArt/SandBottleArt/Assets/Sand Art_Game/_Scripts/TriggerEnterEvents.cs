using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEnterEvents : MonoBehaviour
{
    public List<string> tags = new List<string> ();

    public UnityEvent onTriggerEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (tags.Contains (other.tag))
        {
            onTriggerEnter.Invoke ();
        }
    }
}
