using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class CharacterState
{
    protected CharacterBlank _character;
    protected CharacterIngameController _characterController;
    protected CharacterStateMachine _stateMachine;

    public CharacterState(
        CharacterBlank character, 
        CharacterIngameController characterController,
        CharacterStateMachine stateMachine)
    {
        _character = character;
        _characterController = characterController;
        _stateMachine = stateMachine;
    }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }
}
