using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class CharacterState
{
    protected CharacterBlank _character;
    protected CharacterStateMachine _stateMachine;

    public CharacterState(CharacterBlank character, CharacterStateMachine stateMachine)
    {
        _character = character;
        _stateMachine = stateMachine;
    }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }
}
