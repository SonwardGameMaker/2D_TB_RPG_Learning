using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class InputHandlerNIS : MonoBehaviour, IInputHandler
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset _playerInputs;

    [Header("Action Map Name Reference")]
    [SerializeField] private string _actionMapName = "Player";

    [Header("Action Names")]
    [SerializeField] private string _selectActionName = "Select";
    [SerializeField] private string _interactActionName = "Interact";

    private InputAction _selectAction;
    private InputAction _interactAction;

    private PlayerIngameController _playerIngameController;

    public event Action LMB_Pressed;
    public event Action RMB_Pressed;
    public event Action AttackMode_Pressed;
    public event Action AttackMode_Released;
    public event Action FirstCellButton_Pressed;
    public event Action SecondCellButton_Pressed;
    public event Action CancelButton_Pressed;

    private void Awake()
    {
        _selectAction = _playerInputs.FindActionMap(_actionMapName).FindAction(_selectActionName);
        _interactAction = _playerInputs.FindActionMap(_actionMapName).FindAction(_interactActionName);

        _playerIngameController = GetComponent<PlayerIngameController>();
    }

    private void Start()
    {
        BindActions();
    }

    private void OnDestroy()
    {
        UnbindActions();
    }

    private void BindActions()
    {
        _selectAction.started += (context) => LMB_Pressed?.Invoke();
        _interactAction.started += (context) => RMB_Pressed?.Invoke();
    }
    private void UnbindActions()
    {
        _selectAction.started -= (context) => LMB_Pressed?.Invoke();
        _interactAction.started -= (context) => RMB_Pressed?.Invoke();
    }
}
