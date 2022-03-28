using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class AccessoriesManager : MonoBehaviour
{
    public List<GameObject> accessories = new List<GameObject> ();
    
    void Start()
    {
        InitList();
    }

    [Button]
    void InitList()
    {
        accessories.Clear();
        for(int i = 0; i < transform.childCount; i++)
        {
            accessories.Add(transform.GetChild(i).gameObject);
        }
    }

    public void OffAllAcc()
    {
        for(int i = 0; i < accessories.Count; i++)
        {
            accessories[i].SetActive(false);
            accessories[i].transform.localScale = Vector3.zero;
        }
    }

    public void OnAccessory(int index)
    {
        accessories[index].SetActive(true);
    }
}
