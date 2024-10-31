using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal abstract class PlayerBehaviourScriptContainer
{
    protected PlayerStateMachine _stateMachine;

    public void SetStateMachine(PlayerStateMachine stateMachine)
    {
        if (_stateMachine == null)
            _stateMachine = stateMachine;
    }

    public abstract void Activate();
}
