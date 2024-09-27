using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class CharacterIdleState : CharacterState
{
    public CharacterIdleState(
        CharacterBlank character,
        CharacterIngameController characterController,
        CharacterStateMachine stateMachine) : base(character, characterController, stateMachine)
    {

    }
}
