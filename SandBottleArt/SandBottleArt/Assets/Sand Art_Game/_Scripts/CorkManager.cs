using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class CorkManager : MonoBehaviour
{
    public List<GameObject> corks = new List<GameObject> ();


    void Start()
    {
        InitList();
    }

    [Button]
    void InitList()
    {
        corks.Clear();
        for(int i = 0; i < transform.childCount; i++)
        {
            corks.Add(transform.GetChild(i).gameObject);
        }
    }

    public void OffAllCork()
    {
        for(int i = 0; i < corks.Count; i++)
        {
            corks[i].SetActive(false);
        }
    }
}
