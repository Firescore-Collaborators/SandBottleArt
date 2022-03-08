using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DoubleValueVariables<T> : ScriptableObject
{
    public event System.Action onValue1Changed;
    public event System.Action onValue2Changed;

    [SerializeField]
    private T Value1;
    [SerializeField]
    private T Value2;

    public T value1
    {
        get
        {
            return Value1;
        }
        set
        {
            Value1 = value;
            onValue1Changed?.Invoke();
        }
    }

    public T value2
    {
        get
        {
            return Value2;
        }
        set
        {
            Value2 = value;
            onValue2Changed?.Invoke();
        }
    }
}

[CreateAssetMenu(fileName = "DoubleIntVariable", menuName = "ScriptableObjects/DoubleIntVariable")]
public class DoubleIntVariable : DoubleValueVariables<int>
{
}
