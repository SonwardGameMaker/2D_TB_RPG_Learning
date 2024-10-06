using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputHandler
{
    public event Action LMB_Pressed;
    public event Action RMB_Pressed;
}
