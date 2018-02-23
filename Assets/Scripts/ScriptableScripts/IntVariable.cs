using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variable/Int")]
public class IntVariable : ScriptableObject
{
    [SerializeField]
    private int intValue;
    public int Value
    {
        get
        {
            return intValue;
        }
    }

    [SerializeField]
    public bool resetOnEnable;
    [SerializeField]
    public GameEvent OnValueChanged;

    private void OnEnable()
    {
        if (resetOnEnable)
        {
            Reset();
        }
    }

    public void AddValue (int value)
    {
        intValue += value;
        OnValueChanged.Raise();
    }

    internal void Reset()
    {
        intValue = 0;
    }
}