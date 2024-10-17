using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal abstract class CharacterState
{
    protected GridManager _gridManager;
    protected CharacterIngameController _characterController;
    protected IInputHandler _inputHandler;
    protected CharacterStateMachine _stateMachine;

    public CharacterState(
        GridManager gridManager, 
        CharacterIngameController characterController,
        IInputHandler inputHandler,
        CharacterStateMachine stateMachine)
    {
        _gridManager = gridManager;
        _characterController = characterController;
        _inputHandler = inputHandler;
        _stateMachine = stateMachine;
    }

    public abstract void EnterState();

    public abstract void ExitState();
}
