using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReaderStandartInputs : MonoBehaviour, IInputHandler
{
    public event Action LMB_Pressed;
    public event Action RMB_Pressed;
    public event Action AttackMode_Pressed;
    public event Action AttackMode_Released;
    public event Action FirstCellButton_Pressed;
    public event Action SecondCellButton_Pressed;
    public event Action CancelButton_Pressed;
    public event Action ChangeWeapon_Pressed;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LMB_Pressed?.Invoke();
        }
        if (Input.GetMouseButtonDown(1))
        {
            RMB_Pressed?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            AttackMode_Pressed?.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            AttackMode_Released?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            ChangeWeapon_Pressed?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            FirstCellButton_Pressed?.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            SecondCellButton_Pressed?.Invoke();
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            CancelButton_Pressed?.Invoke();
        }
    }
}
