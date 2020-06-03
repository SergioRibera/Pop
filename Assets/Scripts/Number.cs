using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Number : MonoBehaviour
{
    public int asignedNumber;
    public bool lockedNumber;
    public bool popNumber;
    public int score;

    public event Action<Number> onMouseDown;

    private void OnMouseDown() => onMouseDown?.Invoke(this);
}
