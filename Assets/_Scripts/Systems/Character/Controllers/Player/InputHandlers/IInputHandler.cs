using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputHandler
{
    public event Action LMB_Pressed;
    public event Action RMB_Pressed;

    public event Action FirstCellButton_Pressed;

    public event Action AttackMode_Pressed;
    public event Action AttackMode_Released;

    public event Action CancelButton_Pressed;
}
