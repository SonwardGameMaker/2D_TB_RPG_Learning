using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class CharacterIdleState : CharacterState
{
    public CharacterIdleState(
        GridManager gridManager,
        CharacterIngameController characterController,
        IInputHandler inputHandler,
        CharacterStateMachine stateMachine) : base(gridManager, characterController, inputHandler, stateMachine)
    {

    }

    public override void EnterState()
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState()
    {
        throw new System.NotImplementedException();
    }
}
