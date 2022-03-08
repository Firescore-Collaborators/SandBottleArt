using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    public Material mat;

    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }
    public void ChangeMat()
    {
        rend.material = mat;
    }
}
