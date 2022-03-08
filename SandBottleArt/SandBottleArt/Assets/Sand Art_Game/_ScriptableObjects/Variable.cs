using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Variable<T> : ScriptableObject
{
    public event System.Action<T, T> OnValueChanged;

    [SerializeField]
    private T Value;

    object temp;

    public T value
    {
        get
        {
            return Value;
        }
        set
        {
            temp = Value;
            Value = value;
            OnValueChanged?.Invoke((T)temp, value);
        }
    }

}



