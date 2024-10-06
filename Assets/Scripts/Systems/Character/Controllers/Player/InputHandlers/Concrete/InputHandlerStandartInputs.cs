using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReaderStandartInputs : MonoBehaviour, IInputHandler
{
    public event Action LMB_Pressed;
    public event Action RMB_Pressed;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LMB_Pressed?.Invoke();
        }
        if (Input.GetMouseButtonDown(1))
        {
            RMB_Pressed?.Invoke();
        }
    }
}
