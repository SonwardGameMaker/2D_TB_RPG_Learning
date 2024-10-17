using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class CharacterStateMachine
{
    private CharacterState _currentState;

    public void Setup(CharacterState startingState)
    {
        _currentState = startingState;
        _currentState.EnterState();
    }

    public CharacterState CurrentState { get => _currentState; }

    public void ChangeState(CharacterState newState)
    {
        _currentState.ExitState();
        _currentState = newState;
        _currentState.EnterState();
    }
}
