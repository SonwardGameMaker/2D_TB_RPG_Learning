using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class CharacterStateMachine
{
    private CharacterState _currentState;

    public void Setup(CharacterState startingState)
    {
        _currentState = startingState;
        _currentState.Enter();
    }

    public CharacterState CurrentState { get => _currentState; }

    public void ChangeState(CharacterState newState)
    {
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
    }
}
