using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New IntVariable", menuName = "Scriptable Objects/IntVariable")]
public class IntVariable : ScriptableObject
{  
    public int i;      

    public void Increment() => i++;

    public void Decrement() => i--;

    /// <summary>
    /// Decrements the value if it is greater than 1
    /// </summary>
    public void DecrementToMinOne() => i = Math.Max(i - 1, 1);

    public void Reset() => i = 0;

    public void SetValue(int value) => i = value;

    public void AddValue(int value) => i += value;

    public void AddValue(IntVariable value) => i += value.i;
}