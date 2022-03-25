using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectFollow2D : MonoBehaviour
{


    private void Update()
    {
                transform.position = Input.mousePosition;

    }
}
