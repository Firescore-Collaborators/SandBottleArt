using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Destroy : MonoBehaviour
{

    public float time;

    public UnityEvent onDestroy;
    public void DestroyThis()
    {
        Timer.Delay(time, () =>
        {
            gameObject.SetActive(false);
            onDestroy.Invoke();
        });
    }
}
