using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReaderStandartInputs : MonoBehaviour, IInputHandler
{
    public event Action LMB_Pressed;
    public event Action RMB_Pressed;

    public event Action MouseWheel_UpScroll; // new
    public event Action MouseWheel_DownScroll; // new

    public event Action AttackMode_Pressed;
    public event Action AttackMode_Released;

    public event Action FirstCellButton_Pressed;
    public event Action SecondCellButton_Pressed;
    public event Action CancelButton_Pressed;
    public event Action ChangeWeapon_Pressed;

    public event Action ChangeInventoryTabVisibility_Pressed; // new

    private float _scroll = 0;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            LMB_Pressed?.Invoke();
        if (Input.GetMouseButtonDown(1))
            RMB_Pressed?.Invoke();

        ReadMouseWheel();

        if (Input.GetKeyDown(KeyCode.LeftControl))
            AttackMode_Pressed?.Invoke();
        if (Input.GetKeyUp(KeyCode.LeftControl))
            AttackMode_Released?.Invoke();

        if (Input.GetKeyDown(KeyCode.X))
            ChangeWeapon_Pressed?.Invoke();

        if (Input.GetKeyDown(KeyCode.Alpha1))
            FirstCellButton_Pressed?.Invoke();
        if (Input.GetKeyUp(KeyCode.Alpha2))
            SecondCellButton_Pressed?.Invoke();

        if (Input.GetKeyDown(KeyCode.N))
            ChangeInventoryTabVisibility_Pressed?.Invoke();

        if (Input.GetKeyUp(KeyCode.Escape))
            CancelButton_Pressed?.Invoke();
    }

    private void ReadMouseWheel()
    {
        _scroll = Input.GetAxis("Mouse ScrollWheel");
        if (_scroll > 0f)
            MouseWheel_UpScroll?.Invoke();
        else if (_scroll < 0f)
            MouseWheel_DownScroll?.Invoke();
    }
}
